using System;
using System.Data;

namespace OrderManager.Features.Ribbon.EmailTemplates;

public class EmailTemplateRepository {

    private readonly IDbConnection _connection;

    public EmailTemplateRepository(IDbConnection connection) {
        _connection = connection;
    }

    public EmailTemplateEventDomain GetEmailTemplateById(Guid id) {
        throw new NotImplementedException();
    }

    public void Save(EmailTemplateEventDomain emailTemplate) {

        using var trx = _connection.BeginTransaction();
        var events = emailTemplate.GetEvents();

        foreach (var e in events) {
            e.ApplyEvent(trx);
        }

        trx.Commit();
    }

}


public interface IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction);
}

public record UpdateNameEvent(string Name) : IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction) {
        throw new NotImplementedException();
    }
}

public record UpdateMainRecipientEvent(Recipient recipient) : IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction) {
        throw new NotImplementedException();
    }
}

public record AddCCEvent(DefinedRecipient Recipient) : IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction) {
        throw new NotImplementedException();
    }
}

public record RemoveCCEvent(DefinedRecipient Recipient) : IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction) {
        throw new NotImplementedException();
    }
}

public record AddBCCEvent(DefinedRecipient Recipient) : IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction) {
        throw new NotImplementedException();
    }
}

public record RemoveBCCEvent(DefinedRecipient Recipient) : IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction) {
        throw new NotImplementedException();
    }
}

public record UpdateSubjectEvent(string subject) : IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction) {
        throw new NotImplementedException();
    }
}

public record UpdateBodyEvent(string body) : IDomainEvent {
    public void ApplyEvent(IDbTransaction transaction) {
        throw new NotImplementedException();
    }
}