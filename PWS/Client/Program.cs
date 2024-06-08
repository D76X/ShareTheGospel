using Client;
using Client.Catalogs;
using Client.Pages.Models.Pages;
//using Client.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using AzureStaticWebApps.Blazor.Authentication;
using Client.StaticWebAppsAuthenticationExtensions;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;
using Websites.Razor.ClassLibrary.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration[SharedConstants.ApiPrefix] ?? builder.HostEnvironment.BaseAddress) });

// Services
builder.Services.AddSingleton<ILanguageService, LanguageService>();
//builder.Services.AddSingleton<IUserSettingsService, UserSettingsService>();
//builder.Services.AddTransient<ILocalStorageService, LocalStorageService>();
builder.Services.AddTransient<ICardCatalog, CardCatalog>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<ISearchService, SearchService>();

// Models
builder.Services.AddTransient<IIndexPage, IndexModel>();
builder.Services.AddTransient<IAboutModel, AboutModel>();

// Authentication
builder.Services.AddStaticWebAppsAuthentication();

await builder.Build().RunAsync();
