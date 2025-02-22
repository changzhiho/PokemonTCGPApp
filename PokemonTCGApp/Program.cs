using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PokemonTCGApp;
using PokemonTCGApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Enregistrement des services
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<PokemonService>();
builder.Services.AddScoped<FavoritesService>();

var host = builder.Build();

// Récupération de FavoritesService après la construction de l'application
var favoritesService = host.Services.GetRequiredService<FavoritesService>();
await favoritesService.InitializeAsync(); // Chargement des favoris

await host.RunAsync();
