using Dapper;
using OrderManager.Domain.Emails;
using System.Data;

namespace Infrastructure.Emails.Queries;

public class GetEmailDetailsByIdQuery {
    
    private readonly IDbConnection _connection;

    public GetEmailDetailsByIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<EmailTemplateDetails> GetEmailDetailsById(int id) {

        const string query = @"SELECT ([Id], [Name], [Sender], [Password], [Subject], [Body], [To], [Cc], [Bcc], [ProfileId])
                                FROM [EmailTemplates]
                                RIGHT JOIN [Profiles_Emails] ON EmailTemplates.Id = Profiles_Emails.EmailId
                                WHERE [ProfileId] = @Id";

        var result = await _connection.QuerySingleAsync(query, new { Id = id });

        return new EmailTemplateDetails {
            Id = result.Id,
            Name = result.Name ?? "",
            Sender = result.Sender ?? "",
            Password = result.Password ?? "",
            Subject = result.Subject ?? "",
            Body = result.Body ?? "",
            To = ((string)result.To)?.Split(',').ToList() ?? new(),
            Cc = ((string)result.Cc)?.Split(',').ToList() ?? new(),
            Bcc = ((string)result.Bcc)?.Split(',').ToList() ?? new()
        };

    }

}
