using DemoServer.Data;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var databaseUrl = builder.Configuration.GetValue<string>("DATABASE_URL")
    ?? throw new InvalidOperationException("Unable to load database connection string.");

var databaseUri = new Uri(databaseUrl);
var userInfo = databaseUri.UserInfo.Split(':');
var connectionString = $"Host={databaseUri.Host};Port={databaseUri.Port};Database={databaseUri.AbsolutePath.TrimStart('/')};User Id={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true;Pooling=true;";

builder.Services.AddDbContext<StudentsDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/", (int a, string b) =>
{
    return a.ToString() + b;
});

app.MapGet("/demo", () => "Hello Edo!");

app.MapGet("/html", (string title) =>
{
    return Results.Content($"<h1 style=\"color: green\">{title}</h1>", "text/html");
});

app.MapGet("/students", async (StudentsDbContext db) =>
{
    return await db.Students.ToListAsync();
});

app.MapGet("/create-student", () =>
{
    string form = """
        <form method="post" action="/create-student">
            <input type="text" name="firstName" placeholder="First name">
            <input type="text" name="lastName" placeholder="Last name">
            <input type="submit" value="Create">
        </form>
    """;
    return Results.Content(form, "text/html");
});

app.MapControllers();

app.Run();

public class StudentsController : Controller
{
    private readonly StudentsDbContext _dbContext;

    public StudentsController(StudentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("/create-student")]
    public IActionResult CreateStudent(Student student)
    {
        _dbContext.Students.Add(student);
        _dbContext.SaveChanges();

        return Redirect("/students");
    }
}
