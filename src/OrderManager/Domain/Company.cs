namespace OrderManager.ApplicationCore.Domain;

/// <summary>
/// Represents an entity which can make, sell, or buy a product
/// </summary>
public class Company {
    
    public int Id { get; set;} = default;

    public string Name { get; set;} = default!;

    public string Contact { get; set;} = default!;

    public string AddressLine1 { get; set;} = default!;

    public string AddressLine2 { get; set;} = default!;

    public string AddressLine3 { get; set;} = default!;

    public string City { get; set;} = default!;

    public string State { get; set;} = default!;

    public string PostalCode { get; set; } = default!;

};