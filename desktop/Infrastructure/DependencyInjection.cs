using Infrastructure.Emails;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Emails;
using System.Data;

namespace Infrastructure;

public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) {
        string connString = config.GetConnectionString("SettingsFile");
        return services.AddTransient<IDbConnection>(s => new SqliteConnection(connString))
                    .AddEmail(connString);
    }

    internal static IServiceCollection AddEmail(this IServiceCollection services, string connString)
        => services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>()
                    .AddTransient<IEmailSender, EmailSender>()
                    .AddTransient<OrderManager.ApplicationCore.Emails.EmailQuery.GetEmailById>(s => 
                                                                new Emails
                                                                .EmailQuery(s.GetRequiredService<IDbConnection>())
                                                                .GetEmailById);

}
