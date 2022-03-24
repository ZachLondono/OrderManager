namespace Sales.Implementation.Infrastructure.Persistance;

public class OrderedItems {

    public Guid Id { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public Guid ProductId { get; set; }

    public string Options { get; set; } = string.Empty;

}
