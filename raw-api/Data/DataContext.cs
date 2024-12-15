using Microsoft.EntityFrameworkCore;

namespace RAWAPI.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CommerceToken> CommerceTokens => Set<CommerceToken>();
    public DbSet<MarketingUser> MarketingUsers => Set<MarketingUser>();
    public DbSet<FormDetail> FormDetails => Set<FormDetail>();
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder) { }

    public override Task<int> SaveChangesAsync(CancellationToken ct = new())
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not Entity entity) continue;
            entity.SavedAt = DateTime.UtcNow;
            entity.RowVersion = entity.RowVersion > 1 ? 1 : entity.RowVersion + 1;
        }

        return base.SaveChangesAsync(ct);
    }
}