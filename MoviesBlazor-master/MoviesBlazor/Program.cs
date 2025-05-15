using MoviesBlazor.Clients;
using MoviesBlazor.Components;

var builder = WebApplication.CreateBuilder(args); // Create a new instance of the WebApplication Class

// Add services to the container.
//services are objects that are used to add functionality to the application
//Such as NavigationManager, HttpClient, etc.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();
//RazorComponents are a new feature in ASP.NET Core that allows you to build web applications using C# and HTML

var movieAppUrl = builder.Configuration["movieApiUrl"] ?? throw new Exception("Movie API URL is not set"); //get the URL of the movie app from the configuration file

builder.Services.AddHttpClient<MoviesClient>(client => client.BaseAddress = new Uri(movieAppUrl)); //add the HttpClient service to the container
builder.Services.AddHttpClient<GenresClient>(client => client.BaseAddress = new Uri(movieAppUrl)); //add the HttpClient service to the container

var app = builder.Build(); // Build the application //creates an instance of the WebApplication class

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection(); //redirects HTTP requests to HTTPS

app.UseStaticFiles(); //serves static files such as images, CSS, and JavaScript in the wwwroot folder
app.UseAntiforgery(); //protects the application from cross-site request forgery (CSRF) attacks

app.MapRazorComponents<App>().AddInteractiveServerRenderMode(); //maps the RazorComponents to the App component
//enables server-side rendering of the components

app.Run(); //start the application
