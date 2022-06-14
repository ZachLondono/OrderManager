using Refit;

namespace OrderManager.ApplicationCore.Orders;

public interface IOrderedItemController {

    [Post("/Orders/Items")]
    public Task AddItemToOrder([Body(buffered:true)]AddItemToOrderCommand command);

    public class AddItemToOrderCommand {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public string Options { get; set; } = "{}";
    }

    [Get("/Orders/{orderId}/Items")]
    public Task GetOrderedItemDetails(int orderId);

    [Delete("/Orders/Items/{itemId}")]
    public Task RemoveItemFromOrder(int itemId);

    [Put("/Orders/Items/SetOptionValue")]
    public Task SetOptionValue([Body(true)] SetOptionValueCommnad command);

    public class SetOptionValueCommnad {
        public int ItemId { get; set; }
        public string Option { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    [Put("/Orders/Items/SetQty")]
    public Task SetQty([Body(true)] SetQtyCommnad commnad);

    public class SetQtyCommnad {
        public int ItemId { get; set; }
        public int Qty { get; set; }
    }

}
