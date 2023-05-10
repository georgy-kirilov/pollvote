using Microsoft.EntityFrameworkCore;

using MyWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

string databaseUrl = builder.Configuration.GetValue<string>("DATABASE_URL")
    ?? throw new ArgumentException("Failed to load database URL from configuration.");

var databaseUri = new Uri(databaseUrl);
string db = databaseUri.AbsolutePath.TrimStart('/');
string[] arguments = databaseUri.UserInfo.Split(':', StringSplitOptions.RemoveEmptyEntries);
string host = databaseUri.Host;
string port = databaseUri.Port.ToString();
string user = arguments[0];
string password = arguments[1];

string efConnectionString = $"Host={host};Database={db};Username={user};Password={password};Port={port}";

builder.Services.AddDbContext<CatalogueDbContext>(options => options.UseNpgsql(efConnectionString));

var app = builder.Build();

await using var dbContext = app.Services.GetRequiredService<CatalogueDbContext>();
await dbContext.Database.EnsureCreatedAsync();

app.MapGet("/", () => "Hello World!");

app.MapGet("/products", async (CatalogueDbContext db) => await db.Products.ToListAsync());

app.Run();
