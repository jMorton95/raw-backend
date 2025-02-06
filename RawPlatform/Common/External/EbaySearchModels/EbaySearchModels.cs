using System.Text.Json.Serialization;

namespace RawPlatform.Common.External.EbaySearchModels;

public class EbaySearchResponse
{
    [JsonPropertyName("itemSummaries")]
    public List<ItemSummary> ItemSummaries { get; set; }
}

public class ItemSummary
{
    [JsonPropertyName("itemId")]
    public string ItemId { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("leafCategoryIds")]
    public List<string> LeafCategoryIds { get; set; }

    [JsonPropertyName("categories")]
    public List<Category> Categories { get; set; }

    [JsonPropertyName("image")]
    public Image Image { get; set; }

    [JsonPropertyName("price")]
    public Price Price { get; set; }

    [JsonPropertyName("itemHref")]
    public string ItemHref { get; set; }

    [JsonPropertyName("seller")]
    public Seller Seller { get; set; }

    [JsonPropertyName("condition")]
    public string Condition { get; set; }

    [JsonPropertyName("conditionId")]
    public string ConditionId { get; set; }

    [JsonPropertyName("thumbnailImages")]
    public List<ThumbnailImage> ThumbnailImages { get; set; }

    [JsonPropertyName("shippingOptions")]
    public List<ShippingOption> ShippingOptions { get; set; }

    [JsonPropertyName("buyingOptions")]
    public List<string> BuyingOptions { get; set; }

    [JsonPropertyName("itemWebUrl")]
    public string ItemWebUrl { get; set; }

    [JsonPropertyName("itemLocation")]
    public ItemLocation ItemLocation { get; set; }

    [JsonPropertyName("adultOnly")]
    public bool AdultOnly { get; set; }

    [JsonPropertyName("legacyItemId")]
    public string LegacyItemId { get; set; }

    [JsonPropertyName("availableCoupons")]
    public bool AvailableCoupons { get; set; }

    [JsonPropertyName("itemCreationDate")]
    public string ItemCreationDate { get; set; }

    [JsonPropertyName("topRatedBuyingExperience")]
    public bool TopRatedBuyingExperience { get; set; }

    [JsonPropertyName("priorityListing")]
    public bool PriorityListing { get; set; }

    [JsonPropertyName("listingMarketplaceId")]
    public string ListingMarketplaceId { get; set; }
}

public class Category
{
    [JsonPropertyName("categoryId")]
    public string CategoryId { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; }
}

public class Image
{
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }
}

public class Price
{
    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}

public class Seller
{
    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("feedbackPercentage")]
    public string FeedbackPercentage { get; set; }

    [JsonPropertyName("feedbackScore")]
    public int FeedbackScore { get; set; }

    [JsonPropertyName("sellerAccountType")]
    public string SellerAccountType { get; set; }
}

public class ThumbnailImage
{
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }
}

public class ShippingOption
{
    [JsonPropertyName("shippingCostType")]
    public string ShippingCostType { get; set; }

    [JsonPropertyName("shippingCost")]
    public ShippingCost ShippingCost { get; set; }
}

public class ShippingCost
{
    [JsonPropertyName("value")]
    public string Value { get; set; }

    [JsonPropertyName("currency")]
    public string Currency { get; set; }
}

public class ItemLocation
{
    [JsonPropertyName("postalCode")]
    public string PostalCode { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }
}
