using Dapper;
using OrderManager.Domain.Emails;
using System.Data;

namespace Infrastructure.Emails.Queries;

public class GetEmailDetailsByProfileIdQuery {

    private readonly IDbConnection _connection;

    public GetEmailDetailsByProfileIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<IEnumerable<EmailTemplateDetails>> GetEmailDetailsByProfileId(int profileId) {

        const string query = @"SELECT ([Id], [Name], [Sender], [Password], [Subject], [Body], [To], [Cc], [Bcc], [ProfileId])
                                FROM [EmailTemplates]
                                RIGHT JOIN [Profiles_Emails] ON EmailTemplates.Id = Profiles_Emails.EmailId
                                WHERE [ProfileId] = @Id";

        var result = await _connection.QueryAsync(query, new { Id = profileId });

        return result.Select(p => new EmailTemplateDetails {
            Id = p.Id,
            Name = p.Name ?? "",
            Sender = p.Sender ?? "",
            Password = p.Password ?? "",
            Subject = p.Subject ?? "",
            Body = p.Body ?? "",
            To = ((string)p.To)?.Split(',').ToList() ?? new(),
            Cc = ((string)p.Cc)?.Split(',').ToList() ?? new(),
            Bcc = ((string)p.Bcc)?.Split(',').ToList() ?? new()
        }) ;

    }

}