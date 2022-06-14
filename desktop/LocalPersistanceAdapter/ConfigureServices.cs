using Catalog.Implementation.Infrastructure;
using Manufacturing.Implementation.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sales.Implementation.Infrastructure;

namespace LocalPersistanceAdapter;

public static class ConfigureService {

    public static IServiceCollection AddLocalPersistance(this IServiceCollection services, IConfiguration config) {

        var salesPath = config["DataBase:LocalConfig:Sales"];
        var manufacturingPath = config["DataBase:LocalConfig:Manufacturing"];
        var catalogPath = config["DataBase:LocalConfig:Catalog"];

        return services.AddSales(() => new() {
            PersistanceMode = Sales.Contracts.PersistanceMode.SQLite,
            Connection = new SqliteConnection($@"Data Source={salesPath};")
        }).AddManufacturing(() => new() {
            PersistanceMode = Manufacturing.Contracts.PersistanceMode.SQLite,
            Connection = new SqliteConnection($@"Data Source={manufacturingPath};")
        }).AddCatalog(() => new() {
            PersistanceMode = Catalog.Contracts.PersistanceMode.SQLite,
            Connection = new SqliteConnection($@"Data Source={catalogPath};")
        });

    }

}