using Infrastructure.Emails;
using Infrastructure.Labels;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Emails;
using OrderManager.ApplicationCore.Labels;
using System.Data;

namespace Infrastructure;

public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config) {
        string connString = config.GetConnectionString("SettingsFile");
        return services.AddTransient<IDbConnection>(s => new SqliteConnection(connString))
                    .AddEmail()
                    .AddLabels();
    }

    private static IServiceCollection AddLabels(this IServiceCollection services)
        => services.AddTransient<ILabelFieldMapRepository, LabelFieldMapRepository>()
                    .AddTransient<ILabelPrinter, DymoLabelPrinter>()
                    .AddTransient<OrderManager.ApplicationCore.Labels.LabelQuery.GetLabelById>(s =>
                                                                new Labels.LabelQuery(s.GetRequiredService<IDbConnection>())
                                                                    .GetLabelById);

    private static IServiceCollection AddEmail(this IServiceCollection services)
        => services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>()
                    .AddTransient<IEmailSender, EmailSender>()
                    .AddTransient<OrderManager.ApplicationCore.Emails.EmailQuery.GetEmailById>(s => 
                                                                new Emails
                                                                .EmailQuery(s.GetRequiredService<IDbConnection>())
                                                                .GetEmailById);

}
