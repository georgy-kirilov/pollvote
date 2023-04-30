var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", (int a, string b) =>
{
    return a.ToString() + b;
});

app.MapGet("/demo", () => "Hello Edo!");

app.MapGet("/html", (string title) =>
{
    // Make SQL query request to the DB
    return Results.Content($"<h1 style=\"color: green\">{title}</h1>", "text/html");
});

app.Run();
