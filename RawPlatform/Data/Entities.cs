using System.ComponentModel.DataAnnotations;

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
    public string? EmailAddress { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    public ICollection<FormDetail>? FormDetails { get; set; }
}

public class FormDetail : Entity
{
    public required string Message { get; set; }
    
    public MarketingUser MarketingUser { get; set; }
}