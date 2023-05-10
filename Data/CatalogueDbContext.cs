using Microsoft.EntityFrameworkCore;

namespace MyWebApp.Data;

public sealed class CatalogueDbContext : DbContext
{
    public CatalogueDbContext(DbContextOptions<CatalogueDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}
