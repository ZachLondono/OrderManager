using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Catalog.Implementation.Infrastructure;
using Sales.Implementation.Infrastructure;
using Manufacturing.Implementation.Infrastructure;

[assembly: FunctionsStartup(typeof(Functions.Startup))]
namespace Functions;

public class Startup : FunctionsStartup {

    public override void Configure(IFunctionsHostBuilder builder) {
        builder.Services
                .AddCatalog()
                .AddSales()
                .AddManufacturing();
    }

}