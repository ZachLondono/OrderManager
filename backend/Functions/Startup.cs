using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Catalog.Implementation.Infrastructure;
using Sales.Implementation.Infrastructure;
using Manufacturing.Implementation.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Data;
using System.Data.SqlClient;

[assembly: FunctionsStartup(typeof(Functions.Startup))]
namespace Functions;

public class Startup : FunctionsStartup {

    public override void Configure(IFunctionsHostBuilder builder) {

        var connString = Environment.GetEnvironmentVariable("sqldb_connection", EnvironmentVariableTarget.Process);

        builder.Services
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
                .AddCatalog()
                .AddSales()
                .AddManufacturing()
                .AddTransient<IDbConnection>(s => new SqlConnection(connString));
    }

}