namespace OrderManager.ApplicationCore.Domain;

public class Company {
    
    public int CompanyId { get; set; }

    public string CompanyName { get; set; } = string.Empty;

    public string ContactName { get; set; } = string.Empty;

    public string ContactEmail { get; set; } = string.Empty;

    public string ContactPhone { get; set; } = string.Empty;

    public string AddressLine1 { get; set; } = string.Empty;
    
    public string AddressLine2 { get; set; } = string.Empty;
    
    public string AddressLine3 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string PostalCode { get; set; } = string.Empty;

    public List<Order> Orders { get; set; } = new();

}
