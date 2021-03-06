using System.Collections.Generic;
using System.Linq;

namespace OrderManager.Domain.Companies;

public class Company {

    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public IEnumerable<Contact> Contacts { get; set; } = Enumerable.Empty<Contact>();

    public string Line1 { get; set; } = string.Empty;

    public string Line2 { get; set; } = string.Empty;

    public string Line3 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Zip { get; set; } = string.Empty;

    public string Roles { get; set; } = string.Empty;

}