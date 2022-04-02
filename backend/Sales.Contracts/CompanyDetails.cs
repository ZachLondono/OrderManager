namespace Sales.Contracts;

public class CompanyDetails {

    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;

    public IEnumerable<ContactDetails> Contacts { get; set; } = Enumerable.Empty<ContactDetails>();

    public string Line1 { get; set; } = string.Empty;

    public string Line2 { get; set; } = string.Empty;

    public string Line3 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Zip { get; set; } = string.Empty;

    public string Roles { get; set; } = string.Empty;

}
