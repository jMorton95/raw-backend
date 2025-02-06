using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Options;
using RawPlatform.Common.External;
using RawPlatform.Common.External.EbayLookupModels;
using RawPlatform.Common.External.EbaySearchModels;
using RawPlatform.Config.Models;
using RawPlatform.Data;

namespace RawPlatform.Modules;



public interface IProductEtl
{
    Task<bool> ProcessEbayProducts();
}


public partial class ProductEtl(IOptions<ThirdParty> settings, DataContext db, HttpClient httpClient, IProductApiAuthenticator authenticator, DatabaseLoggingService logger)
    : IProductEtl
{
   public async Task<bool> ProcessEbayProducts()
   {
       var res = await GetEbayProducts();
       
       if (res == null)
           return false;

       var productSearchResults = ExtractProductsFromSearch(res);
       
       var products = await GetAllProductDetails(productSearchResults);
       
       var productIds = products.Select(p => p.EbayId).ToList();
       
       await db.Products.Where(prod => !productIds.Contains(prod.EbayId)).ExecuteDeleteAsync();
       
       var existingProducts = await db.Products.Where(prod => productIds.Contains(prod.EbayId)).ToListAsync();

       var productsToUpdate = products.Where(x => existingProducts.Any(y => x.EbayId == y.EbayId)).ToList();
       var newProducts = products.Where(x => existingProducts.All(y => x.EbayId != y.EbayId)).ToList();
       
       await db.AddRangeAsync(newProducts);

       foreach (var update in productsToUpdate)
       {
           var existingDbProd = existingProducts.FirstOrDefault(x => x.EbayId == update.EbayId);
           
           if (existingDbProd != null)
               continue;
           
           db.Entry(existingDbProd).CurrentValues.SetValues(update);
       }
       
       await db.SaveChangesAsync();
       
       return await Task.FromResult(true);
   }

   private async Task ConfigureHttpClient()
   {
       var settingsValues = settings.Value;
       
       if (settingsValues == null)
       {
           throw new ArgumentNullException(nameof(settings));
       }

       var authToken = await authenticator.GetOAuthToken();
       
       httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
       httpClient.DefaultRequestHeaders.Add("X-EBAY-C-MARKETPLACE-ID", settingsValues.ProductMarketPlaceId!);
   }
   
   private async Task<EbaySearchResponse?> GetEbayProducts()
   {
       await ConfigureHttpClient();
       
       var endpoint = $"{settings.Value.ProductQueryUrl}?limit=100&{settings.Value.ProductFilterString}&category_ids=12576";
       
       await logger.LogInformation<ProductEtl>($"Getting Products from: - {endpoint}");

       try
       {
            var response = await httpClient.GetAsync(endpoint);
            
            await logger.LogInformation<ProductEtl>($"Returned {response.StatusCode}");
       
            var result = await response.Content.ReadFromJsonAsync<EbaySearchResponse>();
            
            await logger.LogInformation<ProductEtl>($"Received {result?.ItemSummaries.Count} Products");
            return result;
       }
       catch (Exception ex)
       {
           await logger.LogError<ProductEtl>("Error occurred searching for products.", ex);
           return null;
       }
   }
   
   private List<ProductSearchResponse> ExtractProductsFromSearch(EbaySearchResponse ebaySearchResponse)
   {
       var products = ebaySearchResponse.ItemSummaries.Select(x => new ProductSearchResponse
       (
           EbayId: x.ItemId,
           EbayPrice: Convert.ToDecimal(x.Price.Value),
           DiscountedPrice: (Convert.ToDecimal(x.Price.Value) * 0.95m),
           Title: x.Title,
           ItemWebUrl: x.ItemWebUrl,
           ItemApiUrl: x.ItemHref
       ));

       return products.ToList();
   }

   private async Task<List<Product>> GetAllProductDetails(List<ProductSearchResponse> products)
   {
       var tasks = products.Select(GetProductDetails);

       var results = new List<Product?>();
       
       await foreach (var res in Task.WhenEach(tasks))
       {
           results.Add(res.Result);
       }
       
       return results.OfType<Product>().ToList();
   }

   private async Task<Product?> GetProductDetails(ProductSearchResponse product)
   {
       var endpoint = product.ItemApiUrl;
       
       var response = await httpClient.GetAsync(endpoint);
       
       var result = await response.Content.ReadFromJsonAsync<EbayItemResponse>();
       
       if (result is null)
           return null;

       var imageResponse = await httpClient.GetStreamAsync(result.Image.ImageUrl);
       
       using var ms = new MemoryStream();
       await imageResponse.CopyToAsync(ms);
       var imageBytes = ms.ToArray();
       var base64String = Convert.ToBase64String(imageBytes);
       
       var descriptionString = MyRegex().Replace(result.Description, string.Empty);
       
       var dbProduct = new Product
       {
           EbayId = product.EbayId,
           EbayPrice = product.EbayPrice,
           Quantity = result.EstimatedAvailabilities.Sum(a => a.EstimatedAvailableQuantity),
           EstimatedAlreadySold = result.EstimatedAvailabilities.Sum(a => a.EstimatedSoldQuantity),
           ListingDate = result.ItemCreationDate,
           DiscountedPrice = product.DiscountedPrice,
           Title = product.Title,
           ConditionDescription = descriptionString,
           ItemWebUrl = product.ItemWebUrl,
           ItemApiUrl = product.ItemApiUrl,
           ProductImageUrl = result.Image.ImageUrl,
           ProductImageBase64 = base64String
       };

       return dbProduct;
   }

   private record ProductSearchResponse(
       string EbayId,
       decimal EbayPrice,
       decimal DiscountedPrice,
       string Title,
       string ItemWebUrl,
       string ItemApiUrl);

    [GeneratedRegex("<.*?>")]
    private static partial Regex MyRegex();
}

