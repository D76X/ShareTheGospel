using Client.Catalogs;
using Microsoft.Extensions.DependencyInjection;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace PWS.Client.Test
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IDependency, DependencyClass>();
            services.AddTransient<ICardCatalog, CardCatalog>();
        }
    }
}
