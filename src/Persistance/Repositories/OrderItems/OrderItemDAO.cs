namespace Persistance.Repositories.OrderItems;

public class OrderItemDAO {

    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Qty { get; set; }

}