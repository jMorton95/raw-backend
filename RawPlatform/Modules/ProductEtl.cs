using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using RawPlatform.Common.External;
using RawPlatform.Config.Models;
using RawPlatform.Data;

namespace RawPlatform.Modules;



public interface IProductEtl
{
    Task<bool> ProcessEbayProducts();
}


public class ProductEtl : IProductEtl  //Inject Endpoint config & auth credentials
{
    private readonly ThirdParty _settings;
    private readonly DataContext _db;
    private readonly HttpClient _httpClient;
    
    public ProductEtl(IOptions<ThirdParty> settings, DataContext db, HttpClient httpClient, IProductApiAuthenticator authenticator)
    {
        _settings = settings.Value;
        _db = db;
        _httpClient = httpClient;
    }
   //Retrieve Auth Token
   
   //Fetch Products - Validate They're from our Seller
   
   //Mark Existing, Non-Matching IDs as Inactive
   
   //Retrieve Matching Products
   
   //Update Matching Products
   
   //Create New Products from new Non-Matching IDs
   
   //Each Time a Product is Updated / Created, need to call full link to retrieve description and images

   public async Task<bool> ProcessEbayProducts()
   {
       return await Task.FromResult(true);
   }
}

