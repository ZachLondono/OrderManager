using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Catalog.Implementation.Infrastructure;
using Sales.Implementation.Infrastructure;
using Manufacturing.Implementation.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs.Host;
using Functions.Endpoints;

[assembly: FunctionsStartup(typeof(Functions.Startup))]
namespace Functions;

public class Startup : FunctionsStartup {

    public override void Configure(IFunctionsHostBuilder builder) {

        var connString = Environment.GetEnvironmentVariable("sqldb_connection", EnvironmentVariableTarget.Process);

        builder.Services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddCatalog(() => new() {
                    PersistanceMode = Catalog.Contracts.PersistanceMode.SQLServer,
                    Connection = new SqlConnection(connString)
                })
                .AddSales(() => new() {
                    PersistanceMode = Sales.Contracts.PersistanceMode.SQLServer,
                    Connection = new SqlConnection(connString)
                })
                .AddManufacturing(() => new() {
                    PersistanceMode = Manufacturing.Contracts.PersistanceMode.SQLServer,
                    Connection = new SqlConnection(connString)
                })
                .AddSingleton<IFunctionFilter, ValidationFilter>();
    }

}