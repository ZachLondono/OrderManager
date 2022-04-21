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

        const string query = @"SELECT [Id], [Name], [Sender], [Password], [Subject], [Body], [To], [Cc], [Bcc]
                                FROM [EmailTemplates]
                                WHERE [Id] = @Id";

        var result = await _connection.QuerySingleAsync(query, new { Id = id });

        return new EmailTemplateDetails {
            Id = (int)result.Id,
            Name = (string)result.Name ?? "",
            Sender = (string)result.Sender ?? "",
            Password = (string)result.Password ?? "",
            Subject = (string)result.Subject ?? "",
            Body = (string)result.Body ?? "",
            To = ((string)result.To)?.Split(',').ToList() ?? new(),
            Cc = ((string)result.Cc)?.Split(',').ToList() ?? new(),
            Bcc = ((string)result.Bcc)?.Split(',').ToList() ?? new()
        };

    }

}
