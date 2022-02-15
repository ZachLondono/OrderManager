using Microsoft.Extensions.DependencyInjection;

namespace Persistance;

public static class DependencyInjection {

    public static IServiceCollection AddPersistance(this IServiceCollection services) {

        IEnumerable<Type> repositories = typeof(DependencyInjection)
                                            .Assembly
                                            .GetTypes()
                                            .Where(t => t.IsSubclassOf(typeof(BaseRepository)) && !t.IsAbstract);

        foreach (Type repository in repositories) {
            string interfaceName = $"I{repository.Name}";
            var repoInterface = repository.GetInterface(interfaceName);

            if (repoInterface is not null)
                services.AddTransient(repoInterface, repository);
            else services.AddTransient(repository);
        }

        return services.AddSingleton<ConnectionStringManager>();

    }

}
