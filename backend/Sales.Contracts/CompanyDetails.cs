namespace Sales.Contracts;

public class CompanyDetails {

    public Guid CompanyId { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public ContactDetails[] Contacts { get; set; } = new ContactDetails[0];

    public string AddressLine1 { get; set; } = string.Empty;

    public string AddressLine2 { get; set; } = string.Empty;

    public string AddressLine3 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Zip { get; set; } = string.Empty;

    public string Roles { get; set; } = string.Empty;

}
