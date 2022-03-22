using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Implementation.Infrastructure;

public static class ConfigureService {
    public static IServiceCollection AddCatalog(IServiceCollection services) =>
        services.AddMediatR(typeof(ConfigureService).Assembly)
                .AddTransient<Contracts.Catalog.GetProducts>((s) => {
                    return () => new Contracts.ProductSummary[0];
                })
                .AddTransient<Contracts.Catalog.GetProduct>((s) => {
                    return (id) => new Contracts.ProductDetails(id, string.Empty, new string[0]);
                });
}