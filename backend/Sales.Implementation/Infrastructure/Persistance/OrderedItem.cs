namespace Sales.Implementation.Infrastructure.Persistance;

public class OrderedItem {

    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int ProductClass { get; set; }

    public int Qty { get; set; }

    public string Options { get; set; } = string.Empty;

}
