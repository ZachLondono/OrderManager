namespace Sales.Implementation.Infrastructure.Persistance;

public class Contact {

    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string? Email { get; set; }

}