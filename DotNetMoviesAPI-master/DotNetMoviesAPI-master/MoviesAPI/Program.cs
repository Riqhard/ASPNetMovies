using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MoviesAPI.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.Intrinsics.X86;
using MoviesAPI.Endpoints;

var builder = WebApplication.CreateBuilder(args);

//Use connection string from appsettings.json
builder.Services.AddDbContext<MovieContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MovieDbCS")));

var app = builder.Build();

//Asynchronous method to seed data into our database
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
       var context = services.GetRequiredService<MovieContext>();
       await context.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error has occured while migrating the database: {ex.Message}");
    }
}

app.MapGet("/", () => "Hello World!");
app.MapMoviesEndpoints();
app.MapGenresEndpoints();

app.Run();


