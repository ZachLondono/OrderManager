using OrderManager.Domain.Emails;
using System.Data;
using Dapper;

namespace Infrastructure.Emails.Queries;

public class GetEmailSummariesByProfileIdQuery {

    private readonly IDbConnection _connection;

    public GetEmailSummariesByProfileIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<IEnumerable<EmailTemplateSummary>> GetEmailSummariesByProfileId(int profileId) {

        const string query = @"SELECT [Id], [Name], [ProfileId]
                                FROM [EmailTemplates]
                                RIGHT JOIN [Profiles_Emails] ON EmailTemplates.Id = Profiles_Emails.EmailId
                                WHERE [ProfileId] = @Id";

        return await _connection.QueryAsync<EmailTemplateSummary>(query, new {
            Id = profileId
        });

    }

}