using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TvShowExplorer.Services;
using TvShowExplorer;
using TvShowExplorer.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Point HttpClient at TVmaze API
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://api.tvmaze.com")
});

builder.Services.AddScoped<TvMazeService>();
builder.Services.AddScoped<FavoritesService>();

await builder.Build().RunAsync();   