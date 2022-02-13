using Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Persistance;

namespace Domain;

public static class DependencyInjection {

    public static IServiceCollection AddDomain(this IServiceCollection services) {

        IEnumerable<Type> entityServices = typeof(DependencyInjection)
                                            .Assembly
                                            .GetTypes()
                                            .Where(t => t.IsSubclassOf(typeof(IEntityService)) && !t.IsAbstract);

        foreach (Type service in entityServices) {
            services.AddTransient(service);
        }

        return services.AddPersistance();

    }

}
