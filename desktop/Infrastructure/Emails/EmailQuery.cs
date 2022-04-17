using Dapper;
using OrderManager.Domain.Emails;
using System.Data;

namespace Infrastructure.Emails;

public class EmailQuery {

    private readonly IDbConnection _connection;

    public EmailQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<EmailTemplate?> GetEmailById(int id) {
        const string query = @"SELECT ([Id], [Name], [Sender], [Password], [Subject], [Body])
                                FROM [EmailTemplates]
                                WHERE [Id] = @Id;";

        EmailDto dto = await _connection.QuerySingleAsync(query, new {
            Id = id
        });


        IEnumerable<string> to = dto.To?.Split(',') ?? Enumerable.Empty<string>();
        IEnumerable<string> cc = dto.Cc?.Split(',') ?? Enumerable.Empty<string>();
        IEnumerable<string> bcc = dto.Bcc?.Split(',') ?? Enumerable.Empty<string>();
        return new(id,
                dto.Name ?? "",
                dto.Sender ?? "",
                dto.Password ?? "",
                dto.Subject ?? "",
                dto.Body ?? "",
                to,
                cc,
                bcc);
    }

    class EmailDto {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Sender { get; set; }
        public string? Password { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? To { get; set; }
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
    }

}
