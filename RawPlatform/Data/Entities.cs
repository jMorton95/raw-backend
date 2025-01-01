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
    [StringLength(100)]
    public string? EmailAddress { get; set; }
    
    [StringLength(30)]
    public string? FirstName { get; set; }
    
    [StringLength(30)]
    public string? LastName { get; set; }
    
    [StringLength(50)]
    public string? PhoneNumber { get; set; }
    
    public ICollection<FormDetail>? FormDetails { get; set; }
}

public class FormDetail : Entity
{
    [StringLength(1000)]
    public required string Message { get; set; }
    
    public MarketingUser MarketingUser { get; set; }
}

public class LogEntry : Entity
{
    public LogLevel LogLevel { get; set; }
    
    [MaxLength(int.MaxValue)]
    public string? Message { get; set; }
    
    [MaxLength(int.MaxValue)]
    public string? Exception { get; set; }
}