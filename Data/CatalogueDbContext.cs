using Microsoft.EntityFrameworkCore;

using Npgsql.NameTranslation;

namespace MyWebApp.Data;

public sealed class CatalogueDbContext : DbContext
{
    public CatalogueDbContext(DbContextOptions<CatalogueDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var newTableName = NpgsqlSnakeCaseNameTranslator.ConvertToSnakeCase(entityType.DisplayName());

            entityType.SetTableName(newTableName);
        }
    }
}
