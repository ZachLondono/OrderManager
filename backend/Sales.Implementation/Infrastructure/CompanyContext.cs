using Sales.Implementation.Domain;

namespace Sales.Implementation.Infrastructure;

internal record ContactAddedEvent(Contact contact);
internal record ContactRemovedEvent(Contact contact);
internal record AddressSetEvent(string Line1, string Line2, string Line3, string City, string State, string Zip);
internal record NameSetEvent(string Name);
internal record RoleAddedEvent(string Role);
internal record RoleRemovedEvent(string Role);

internal class CompanyContext {

    private readonly Company _company;
    private readonly List<object> _events;

    public CompanyContext(Company company) {
        _company = company;
        _events = new();
    }

    public void AddContact(Contact contact) => throw new NotImplementedException();

    public void RemoveContact(Contact contact) => throw new NotImplementedException();

    public void SetAddress(string Line1, string Line2, string Line3, string City, string State, string Zip) => throw new NotImplementedException();

    public void SetName(string name) => throw new NotImplementedException();

    public void AddRole(string role) => throw new NotImplementedException();

    public void RemoveRole(string role) => throw new NotImplementedException();

}