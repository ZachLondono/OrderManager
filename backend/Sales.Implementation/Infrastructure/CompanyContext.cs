using Sales.Implementation.Domain;

namespace Sales.Implementation.Infrastructure;

internal record ContactAddedEvent(Contact Contact);
internal record ContactRemovedEvent(int ContactId);
internal record ContactUpdatedEvent(Contact Contact);
internal record AddressSetEvent(Address Address);
internal record NameSetEvent(string Name);
internal record RoleAddedEvent(CompanyRole Role);
internal record RoleRemovedEvent(CompanyRole Role);

public class CompanyContext {

    private readonly Company _company;
    private readonly List<object> _events;

    public IReadOnlyCollection<object> Events => _events.AsReadOnly();
    public int Id => _company.Id;

    public CompanyContext(Company company) {
        _company = company;
        _events = new();
    }

    public void AddContact(Contact contact) {
        _company.AddContact(contact);
        _events.Add(new ContactAddedEvent(contact));
    }

    public void RemoveContact(int contactId) {
        _company.RemoveContact(contactId);
        _events.Add(new ContactRemovedEvent(contactId));
    }

    public void UpdateContact(Contact contact) {
        _company.UpdateContact(contact);
        _events.Add(new ContactUpdatedEvent(contact));
    }

    public void SetAddress(string Line1, string Line2, string Line3, string City, string State, string Zip) {
        _company.Address = new() {
            Line1 = Line1,
            Line2 = Line2,
            Line3 = Line3,
            City = City,
            State = State,
            Zip = Zip
        };
        _events.Add(new AddressSetEvent(_company.Address));
    }

    public void SetName(string name) {
        _company.Name = name;
        _events.Add(new NameSetEvent(name));
    }

    public void AddRole(CompanyRole role) {
        _company.AddRole(role);
        _events.Add(new RoleAddedEvent(role));
    }

    public void RemoveRole(CompanyRole role) {
        _company.RemoveRole(role);
        _events.Add(new RoleRemovedEvent(role));
    }

}