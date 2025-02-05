using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using RawPlatform.Common.External;
using RawPlatform.Config.Models;
using RawPlatform.Data;

namespace RawPlatform.Modules;



public interface IProductEtl
{
    Task<bool> ProcessEbayProducts();
}


public class ProductEtl(IOptions<ThirdParty> settings, DataContext db, HttpClient httpClient, IProductApiAuthenticator authenticator)
    : IProductEtl
{
    //Retrieve Auth Token
   
   //Fetch Products - Validate They're from our Seller
   
   //Mark Existing, Non-Matching IDs as Inactive
   
   //Retrieve Matching Products
   
   //Update Matching Products
   
   //Create New Products from new Non-Matching IDs
   
   //Each Time a Product is Updated / Created, need to call full link to retrieve description and images

   public async Task<bool> ProcessEbayProducts()
   {
       var res = await GetEbayProducts();

       var productSearchResults = ExtractProductsFromSearch(res);
       
       var products = GetAllProductDetails(productSearchResults);
       
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
       
       var response = await httpClient.GetAsync(endpoint);
       
       var result = await response.Content.ReadFromJsonAsync<EbaySearchResponse>();
       
       return result;
   }
   
   private List<ProductSearchResponse> ExtractProductsFromSearch(EbaySearchResponse ebaySearchResponse)
   {
       var products = ebaySearchResponse.ItemSummaries.Select(x => new ProductSearchResponse
       (
           EbayId: x.ItemId,
           EbayPrice: Convert.ToDecimal(x.Price.Value),
           DiscountedPrice: (Convert.ToDecimal(x.Price.Value) * 0.95m),
           Title: x.Title,
           ConditionDescription: x.Condition,
           ItemWebUrl: x.ItemWebUrl,
           ItemApiUrl: x.ItemHref
       ));

       return products.ToList();
   }

   private async Task<List<Product>> GetAllProductDetails(List<ProductSearchResponse> products)
   {
       var tasks = products.Select(GetProductDetails);

       var results = new List<Product>();
       
       await foreach (var res in Task.WhenEach(tasks))
       {
           results.Add(res.Result);
       }
       
       return results;
   }

   private async Task<Product> GetProductDetails(ProductSearchResponse product)
   {
       var endpoint = product.ItemApiUrl;
       
       var response = await httpClient.GetAsync(endpoint);
       
       var result = await response.Content.ReadAsStringAsync();

       return new Product();
   }

   public record ProductSearchResponse(
       string EbayId,
       decimal EbayPrice,
       decimal DiscountedPrice,
       string Title,
       string ConditionDescription,
       string ItemWebUrl,
       string ItemApiUrl);
}

