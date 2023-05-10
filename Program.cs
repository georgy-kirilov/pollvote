using Microsoft.EntityFrameworkCore;

using MyWebApp;
using MyWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCatalogueDbContext(builder.Configuration);

var app = builder.Build();

await using var dbContext = app.Services.GetRequiredService<CatalogueDbContext>();
await dbContext.Database.EnsureDeletedAsync();
await dbContext.Database.EnsureCreatedAsync();

app.MapGet("/", () => "Hello World!");

app.MapGet("/products", async (CatalogueDbContext db) => await db.Products.ToListAsync());

app.Run();
