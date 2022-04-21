using Infrastructure.Emails;
using Infrastructure.Emails.Queries;
using Infrastructure.Labels;
using Infrastructure.Labels.Queries;
using Infrastructure.Profiles;
using Infrastructure.Profiles.Queries;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderManager.ApplicationCore.Emails;
using OrderManager.ApplicationCore.Labels;
using OrderManager.ApplicationCore.Profiles;
using System.Data;

namespace Infrastructure;

public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services/*, IConfiguration config */) {
        string connString = "";//config.GetConnectionString("SettingsFile");
        return services.AddTransient<IDbConnection>(s => new SqliteConnection(connString))
                    .AddProfiles()
                    .AddEmail()
                    .AddLabels();
    }

    private static IServiceCollection AddProfiles(this IServiceCollection services)
        => services.AddTransient<IReleaseProfileRepository, ReleaseProfileRepository>()
                    .AddTransient<ReleaseService>()
                    .AddTransient<ProfileQuery.GetProfileById>(s => new GetProfileByIdQuery(s.GetRequiredService<IDbConnection>()).GetProfileById)
                    .AddTransient<ProfileQuery.GetProfileSummaries>(s => new GetProfileSummariesQuery(s.GetRequiredService<IDbConnection>()).GetProfileSummaries)
                    .AddTransient<ProfileQuery.GetProfileDetailsById>(s => new GetProfileDetailsByIdQuery(s.GetRequiredService<IDbConnection>()).GetProfileDetailsById);

    private static IServiceCollection AddLabels(this IServiceCollection services)
        => services.AddTransient<ILabelFieldMapRepository, LabelFieldMapRepository>()
                    .AddTransient<ILabelPrinter, DymoLabelPrinter>()
                    .AddTransient<LabelService>()
                    .AddTransient<LabelQuery.GetLabelById>(s => new GetLabelByIdQuery(s.GetRequiredService<IDbConnection>()).GetLabelById)
                    .AddTransient<LabelQuery.GetLabelSummaries>(s => new GetLabelSummariesQuery(s.GetRequiredService<IDbConnection>()).GetLabelSummaries)
                    .AddTransient<LabelQuery.GetLabelSummariesByProfileId>(s => new GetLabelSummariesByProfileIdQuery(s.GetRequiredService<IDbConnection>()).GetLabelSummariesByProfileId)
                    .AddTransient<LabelQuery.GetLabelDetailsById>(s => new GetLabelDetailsByIdQuery(s.GetRequiredService<IDbConnection>()).GetLabelDetailsById)
                    .AddTransient<LabelQuery.GetLabelDetailsById>(s => new GetLabelDetailsByIdQuery(s.GetRequiredService<IDbConnection>()).GetLabelDetailsById);

    private static IServiceCollection AddEmail(this IServiceCollection services)
        => services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>()
                    .AddTransient<IEmailSender, EmailSender>()
                    .AddTransient<EmailService>()
                    .AddTransient<EmailQuery.GetEmailById>(s => new GetEmailByIdQuery(s.GetRequiredService<IDbConnection>()).GetEmailById)
                    .AddTransient<EmailQuery.GetEmailSummaries>(s => new GetEmailSummariesQuery(s.GetRequiredService<IDbConnection>()).GetEmailSummaries)
                    .AddTransient<EmailQuery.GetEmailSummariesByProfileId>(s => new GetEmailSummariesByProfileIdQuery(s.GetRequiredService<IDbConnection>()).GetEmailSummariesByProfileId)
                    .AddTransient<EmailQuery.GetEmailDetailsById>(s => new GetEmailDetailsByIdQuery(s.GetRequiredService<IDbConnection>()).GetEmailDetailsById)
                    .AddTransient<EmailQuery.GetEmailDetailsByProfileId>(s => new GetEmailDetailsByProfileIdQuery(s.GetRequiredService<IDbConnection>()).GetEmailDetailsByProfileId);

}
