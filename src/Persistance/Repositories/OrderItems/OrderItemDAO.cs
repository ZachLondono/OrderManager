namespace Persistance.Repositories.OrderItems;

public class OrderItemDAO {

    public int Id { get; set; }

    public Guid OrderId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public int Qty { get; set; }

    public Dictionary<string, string> Options { get; set; } = new();

}