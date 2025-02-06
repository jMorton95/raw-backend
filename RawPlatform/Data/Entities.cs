using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RawPlatform.Modules;

namespace RawPlatform.Data;

public abstract class Entity
{
    [Required, Key]
    public int Id { get; set; }
    
    [Required]
    public DateTime SavedAt { get; set; }
    
    [Required]
    public int RowVersion { get; set; }
}

public class CommerceToken : Entity
{
    [Required]
    public required string Token { get; set; }
    
    [Required]
    public DateTime IssuedAt { get; set; }
    [Required]
    public DateTime Expires { get; set; }
}

public class MarketingUser : Entity
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
    public string? EmailAddress { get; set; }
    
    [Required(ErrorMessage = "Email is required")]
    [StringLength(100, ErrorMessage = "Email Address cannot exceed 100 characters")]
    public string? Name { get; set; }
    
    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(50, ErrorMessage = "Phone number cannot exceed 50 characters")]
    public string? PhoneNumber { get; set; }
    
    [Required(ErrorMessage = "Message is required")]
    [StringLength(1000, ErrorMessage = "Message cannot exceed 1000 characters")]
    public string? Message { get; set; }
}

public class LogEntry : Entity
{
    public LogLevel LogLevel { get; set; }
    
    [MaxLength(int.MaxValue)]
    public string? Message { get; set; }
    
    [MaxLength(int.MaxValue)]
    public string? Exception { get; set; }
}

public class Product : Entity
{
    public string EbayId { get; set; }
    
    public decimal EbayPrice { get; set; }
    
    public decimal? DiscountedPrice { get; set; }
    
    public int Quantity { get; set; }

    public string Title { get; set; }
    
    public string ConditionDescription { get; set; }
    
    public DateTime ListingDate { get; set; }
    
    public string ItemWebUrl { get; set; }
    
    public string ItemApiUrl { get; set; }
    
    public int EstimatedAlreadySold { get; set; }

    public string ProductImageUrl { get; set; }
    
    public string ProductImageBase64 { get; set; }
}