using System.Collections.ObjectModel;

namespace Sales.Implementation.Domain;

public enum CompanyRole {
    Customer,
    Vendor,
    Supplier
}

public class Company {
    
    public Guid Id { get; init; }
    
    public string Name { get; set; }

    private readonly List<Contact> _contacts;
    public IReadOnlyCollection<Contact> Contacts => _contacts;
    
    public Address Address { get; set; }

    private readonly List<CompanyRole> _roles;
    public ReadOnlyCollection<CompanyRole> Roles => _roles.AsReadOnly();

    public Company(Guid id, string name) {
        Id = id;
        Name = name;
        Address = new();
        _roles = new();
        _contacts = new();
    }

    public Company(Guid id, string name, List<Contact> contacts, Address adderss, List<CompanyRole> roles) {
        Id = id;
        Name = name;
        Address = adderss;
        _roles = roles;
        _contacts = contacts;
    }

    public void AddRole(CompanyRole role) {
        if (_roles.Contains(role))
            throw new InvalidOperationException($"Company already has the '{role}' role");

        _roles.Add(role);
    }

    public void RemoveRole(CompanyRole role) {
        _roles.Remove(role);
    }

    public void AddContact(Contact contact) {
        _contacts.Add(contact);
    }

    public void RemoveContact(Contact contact) {
        _contacts.Remove(contact);
    }

}