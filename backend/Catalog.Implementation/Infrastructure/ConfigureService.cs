using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Catalog.Contracts;

namespace Catalog.Implementation.Infrastructure;

public static class ConfigureService {
    public static IServiceCollection AddCatalog(this IServiceCollection services, Func<CatalogSettings> configureSettings) =>
        services.AddMediatR(typeof(ConfigureService).Assembly)
                .AddValidatorsFromAssembly(typeof(ConfigureService).Assembly)
                .AddTransient(s => configureSettings())
                .AddTransient<Application.Catalog>()
                .AddTransient<CatalogProducts.GetProducts>((s) =>
                    s.GetRequiredService<Application.Catalog>().GetProducts)
                .AddTransient<CatalogProducts.GetProductDetails>((s) =>
                    s.GetRequiredService<Application.Catalog>().GetProductDetails)
                .AddTransient<CatalogProducts.GetProductClass>((s) =>
                    s.GetRequiredService<Application.Catalog>().GetProductClass);
}