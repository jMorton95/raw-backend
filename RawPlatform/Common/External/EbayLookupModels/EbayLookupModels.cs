using System.Text.Json.Serialization;

namespace RawPlatform.Common.External.EbayLookupModels;

public class EbayItemResponse
{
    [JsonPropertyName("itemId")]
    public string ItemId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("price")]
    public Price Price { get; set; }

    [JsonPropertyName("categoryPath")]
    public string CategoryPath { get; set; }

    [JsonPropertyName("categoryIdPath")]
    public string CategoryIdPath { get; set; }

    [JsonPropertyName("condition")]
    public string Condition { get; set; }

    [JsonPropertyName("conditionId")]
    public string ConditionId { get; set; }

    [JsonPropertyName("itemLocation")]
    public ItemLocation ItemLocation { get; set; }

    [JsonPropertyName("image")]
    public Image Image { get; set; }

    [JsonPropertyName("brand")]
    public string Brand { get; set; }

    [JsonPropertyName("itemCreationDate")]
    public DateTime ItemCreationDate { get; set; }

    [JsonPropertyName("seller")]
    public Seller Seller { get; set; }

    [JsonPropertyName("gtin")]
    public string Gtin { get; set; }

    [JsonPropertyName("estimatedAvailabilities")]
    public List<EstimatedAvailability> EstimatedAvailabilities { get; set; }

    [JsonPropertyName("shipToLocations")]
    public ShipToLocations ShipToLocations { get; set; }

    [JsonPropertyName("returnTerms")]
    public ReturnTerms ReturnTerms { get; set; }

    [JsonPropertyName("taxes")]
    public List<Tax> Taxes { get; set; }

    [JsonPropertyName("localizedAspects")]
    public List<LocalizedAspect> LocalizedAspects { get; set; }

    [JsonPropertyName("topRatedBuyingExperience")]
    public bool TopRatedBuyingExperience { get; set; }

    [JsonPropertyName("buyingOptions")]
    public List<string> BuyingOptions { get; set; }

    [JsonPropertyName("itemWebUrl")]
    public string ItemWebUrl { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("paymentMethods")]
    public List<PaymentMethod> PaymentMethods { get; set; }

    [JsonPropertyName("immediatePay")]
    public bool ImmediatePay { get; set; }

    [JsonPropertyName("enabledForGuestCheckout")]
    public bool EnabledForGuestCheckout { get; set; }

    [JsonPropertyName("eligibleForInlineCheckout")]
    public bool EligibleForInlineCheckout { get; set; }

    [JsonPropertyName("lotSize")]
    public int LotSize { get; set; }

    [JsonPropertyName("legacyItemId")]
    public string LegacyItemId { get; set; }

    [JsonPropertyName("priorityListing")]
    public bool PriorityListing { get; set; }

    [JsonPropertyName("adultOnly")]
    public bool AdultOnly { get; set; }

    [JsonPropertyName("categoryId")]
    public string CategoryId { get; set; }

    [JsonPropertyName("listingMarketplaceId")]
    public string ListingMarketplaceId { get; set; }
}

public class Price
{
    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }

    [JsonPropertyName("convertedFromValue")]
    public string ConvertedFromValue { get; set; }

    [JsonPropertyName("convertedFromCurrency")]
    public string ConvertedFromCurrency { get; set; }
}

public class ItemLocation
{
    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("stateOrProvince")]
    public string StateOrProvince { get; set; }

    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }
}

public class Image
{
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("width")]
    public int Width { get; set; }

    [JsonPropertyName("height")]
    public int Height { get; set; }
}

public class Seller
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("feedbackPercentage")]
    public string FeedbackPercentage { get; set; }

    [JsonPropertyName("feedbackScore")]
    public int FeedbackScore { get; set; }

    [JsonPropertyName("sellerLegalInfo")]
    public object SellerLegalInfo { get; set; }
}

public class EstimatedAvailability
{
    [JsonPropertyName("estimatedAvailabilityStatus")]
    public string EstimatedAvailabilityStatus { get; set; }

    [JsonPropertyName("estimatedAvailableQuantity")]
    public int EstimatedAvailableQuantity { get; set; }

    [JsonPropertyName("estimatedSoldQuantity")]
    public int EstimatedSoldQuantity { get; set; }

    [JsonPropertyName("estimatedRemainingQuantity")]
    public int EstimatedRemainingQuantity { get; set; }
}

public class ShipToLocations
{
    [JsonPropertyName("regionIncluded")]
    public List<Region> RegionIncluded { get; set; }

    [JsonPropertyName("regionExcluded")]
    public List<Region> RegionExcluded { get; set; }
}

public class Region
{
    [JsonPropertyName("regionName")]
    public string RegionName { get; set; }

    [JsonPropertyName("regionType")]
    public string RegionType { get; set; }

    [JsonPropertyName("regionId")]
    public string RegionId { get; set; }
}

public class ReturnTerms
{
    [JsonPropertyName("returnsAccepted")]
    public bool ReturnsAccepted { get; set; }
}

public class Tax
{
    [JsonPropertyName("taxJurisdiction")]
    public TaxJurisdiction TaxJurisdiction { get; set; }

    [JsonPropertyName("taxType")]
    public string TaxType { get; set; }

    [JsonPropertyName("shippingAndHandlingTaxed")]
    public bool ShippingAndHandlingTaxed { get; set; }

    [JsonPropertyName("includedInPrice")]
    public bool IncludedInPrice { get; set; }

    [JsonPropertyName("ebayCollectAndRemitTax")]
    public bool EbayCollectAndRemitTax { get; set; }
}

public class TaxJurisdiction
{
    [JsonPropertyName("region")]
    public Region Region { get; set; }

    [JsonPropertyName("taxJurisdictionId")]
    public string TaxJurisdictionId { get; set; }
}

public class LocalizedAspect
{
    [JsonPropertyName("type")]
    public string Type { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class PaymentMethod
{
    [JsonPropertyName("paymentMethodType")]
    public string PaymentMethodType { get; set; }

    [JsonPropertyName("paymentMethodBrands")]
    public List<PaymentMethodBrand> PaymentMethodBrands { get; set; }
}

public class PaymentMethodBrand
{
    [JsonPropertyName("paymentMethodBrandType")]
    public string PaymentMethodBrandType { get; set; }
}