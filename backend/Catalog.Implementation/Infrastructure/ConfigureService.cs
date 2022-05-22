using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Implementation.Infrastructure;

public static class ConfigureService {
    public static IServiceCollection AddCatalog(this IServiceCollection services) =>
        services.AddMediatR(typeof(ConfigureService).Assembly)
                .AddValidatorsFromAssembly(typeof(ConfigureService).Assembly)
                .AddTransient<Application.Catalog>()
                .AddTransient<Contracts.CatalogProducts.GetProducts>((s) =>
                    s.GetRequiredService<Application.Catalog>().GetProducts)
                .AddTransient<Contracts.CatalogProducts.GetProductDetails>((s) =>
                    s.GetRequiredService<Application.Catalog>().GetProductDetails);
}