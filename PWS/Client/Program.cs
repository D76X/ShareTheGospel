using Client;
using Client.Abstractions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
//using AzureStaticWebApps.Blazor.Authentication;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services
	.AddScoped(sp => new HttpClient
	{
		BaseAddress = new Uri(builder.Configuration[Constants.ApiPrefix] ??
		                      builder.HostEnvironment.BaseAddress)
	});
	//.AddStaticWebAppsAuthentication();

await builder.Build().RunAsync();
