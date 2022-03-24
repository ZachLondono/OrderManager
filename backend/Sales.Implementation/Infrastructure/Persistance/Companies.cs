namespace Sales.Implementation.Infrastructure.Persistance;

public class Companies {

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Roles { get; set; } = string.Empty;

    public string Line1 { get; set; } = string.Empty;

    public string Line2 { get; set; } = string.Empty;

    public string Line3 { get; set; } = string.Empty;

    public string City { get; set; } = string.Empty;

    public string State { get; set; } = string.Empty;

    public string Zip { get; set; } = string.Empty;

}
