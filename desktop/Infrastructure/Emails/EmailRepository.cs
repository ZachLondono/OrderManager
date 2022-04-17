using Dapper;
using OrderManager.ApplicationCore.Emails;
using System.Data;

namespace Infrastructure.Emails;

public class EmailTemplateRepository : IEmailTemplateRepository {

    private readonly IDbConnection _connection;

    public EmailTemplateRepository(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<EmailTemplateContext> Add(string name) {
        const string sql = @"INSERT INTO [EmailTemplates] ([Name])
                            VALUES (@Name)
                            returning id";
        int newId = await _connection.QuerySingleAsync<int>(sql, new { 
            Name = name }
        );

        return new(new(newId, name));
    }

    public async Task<EmailTemplateContext> GetById(int id) {
        var query = new EmailQuery(_connection);
        var template = await query.GetEmailById(id);

        if (template is null) throw new InvalidDataException($"Email template with Id '{id}' could not be found");
        return new(template);
    }

    public async Task Remove(int id) {
        const string sql = @"DELETE FROM [EmailTemplates] WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = id
        });
    }

    public async Task Save(EmailTemplateContext context) {

        _connection.Open();
        var trx = _connection.BeginTransaction();
        var events = context.Events;

        foreach (var ev in events) {

            if (ev is EmailNameChangedEvent nameChange) {
                await ApplyNameChangedEvent(trx, context.Id, nameChange);
            } else if (ev is EmailBodyChangedEvent bodyChange) {
                await ApplyBodyChangedEvent(trx, context.Id, bodyChange);
            }else if (ev is EmailSubjectChangedEvent subjectChange) {
                await ApplySubjectChangedEvent(trx, context.Id, subjectChange);
            }else if (ev is EmailToAddedEvent toAdded) {
                await ApplyToAddedEvent(trx, context.Id, toAdded);
            }else if (ev is EmailToRemovedEvent toRemoved) {
                await ApplyToRemovedEvent(trx, context.Id, toRemoved);
            }else if (ev is EmailCcAddedEvent ccAdded) {
                await ApplyCcAddedEvent(trx, context.Id, ccAdded);
            }else if (ev is EmailCcRemovedEvent ccRemoved) {
                await ApplyCcRemovedEvent(trx, context.Id, ccRemoved);
            }else if (ev is EmailBccAddedEvent bccAdded) {
                await ApplyBccAddedEvent(trx, context.Id, bccAdded);
            }else if (ev is EmailBccRemovedEvent bccRemoved) {
                await ApplyBccRemovedEvent(trx, context.Id, bccRemoved);
            }
        }

        trx.Commit();

    }

    private async Task ApplyNameChangedEvent(IDbTransaction trx, int emailId, EmailNameChangedEvent ev) {
        string sql = $"UPDATE [EmailTemplates] SET [Name] = @Name WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = emailId,
            Subject = ev.Name
        }, trx);
    }

    private async Task ApplyBodyChangedEvent(IDbTransaction trx, int emailId, EmailBodyChangedEvent ev) {
        string sql = $"UPDATE [EmailTemplates] SET [Body] = @Body WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = emailId,
            Subject = ev.Body
        }, trx);
    }

    private async Task ApplySubjectChangedEvent(IDbTransaction trx, int emailId, EmailSubjectChangedEvent ev) {
        string sql = $"UPDATE [EmailTemplates] SET [Subject] = @Subject WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = emailId,
            Subject = ev.Subject
        }, trx);
    }

    private async Task ApplyToAddedEvent(IDbTransaction trx, int emailId, EmailToAddedEvent ev) {
        await QueryAndAppend(trx, emailId, "To", ev.To);
    }

    private async Task ApplyToRemovedEvent(IDbTransaction trx, int emailId, EmailToRemovedEvent ev) {
        await QueryAndRemove(trx, emailId, "To", ev.To);
    }

    private async Task ApplyCcAddedEvent(IDbTransaction trx, int emailId, EmailCcAddedEvent ev) {
        await QueryAndAppend(trx, emailId, "Cc", ev.Cc);
    }

    private async Task ApplyCcRemovedEvent(IDbTransaction trx, int emailId, EmailCcRemovedEvent ev) {
        await QueryAndRemove(trx, emailId, "Cc", ev.Cc);
    }

    private async Task ApplyBccAddedEvent(IDbTransaction trx, int emailId, EmailBccAddedEvent ev) {
        await QueryAndAppend(trx, emailId, "Bcc", ev.Bcc);
    }

    private async Task ApplyBccRemovedEvent(IDbTransaction trx, int emailId, EmailBccRemovedEvent ev) {
        await QueryAndRemove(trx, emailId, "Bcc", ev.Bcc);
    }

    private async Task QueryAndAppend(IDbTransaction trx, int emailId, string column, string value) {
        string query = $"SELECT ([{column}]) FROM [EmailTemplates] WHERE [Id] = @Id;";
        string data = await _connection.QuerySingleAsync<string>(query, new {
            Id = emailId
        }, trx);

        if (string.IsNullOrEmpty(data))
            data = value;
        else data += $",{value}";

        string sql = $"UPDATE [EmailTemplates] SET [{column}] = @NewVal WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = emailId,
            NewValue = data
        }, trx);
    }

    private async Task QueryAndRemove(IDbTransaction trx, int emailId, string column, string value) {
        string query = $"SELECT ([{column}]) FROM [EmailTemplates] WHERE [Id] = @Id;";
        string data = await _connection.QuerySingleAsync<string>(query, new {
            Id = emailId
        }, trx);

        if (string.IsNullOrEmpty(data))
            return;

        var updatedData = data.Split(',')
                        .ToList()
                        .Remove(value);

        data = string.Join(',', updatedData);

        string sql = $"UPDATE [EmailTemplates] SET [{column}] = @NewVal WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = emailId,
            NewValue = data
        }, trx);
    }

}