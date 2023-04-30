var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/", (int a, string b) =>
{
    return a.ToString() + b;
});

app.MapGet("/demo", () => "Hello Edo!");

app.Run();
