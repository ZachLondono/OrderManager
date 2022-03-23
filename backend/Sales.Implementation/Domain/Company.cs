namespace Sales.Implementation.Domain;

public enum CompanyRole {
    Customer,
    Vendor,
    Supplier
}

public class Company {
    
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public List<Contact> Contacts { get; set; }
    
    public Address Address { get; set; }
    
    public CompanyRole[] Roles { get; set; }

}
