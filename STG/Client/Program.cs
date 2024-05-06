using System;
using System.Net.Http;
using Client;
using Client.Abstractions.Services;
using Client.Pages.Models.Pages;
using Client.Services;
using Client.Services.UserSettingsService;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Websites.Razor.ClassLibrary.Abstractions.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<ILanguageService, LanguageService>();
builder.Services.AddSingleton<IUserSettingsService, UserSettingsService>();
builder.Services.AddTransient<ILocalStorageService, LocalStorageService>();
builder.Services.AddTransient<ICardService, CardService>();
builder.Services.AddTransient<IIndexPage, IndexModel>();

builder.Services.AddTransient<IAboutModel,AboutModel>();

await builder.Build().RunAsync();
