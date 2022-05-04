using OrderManager.Domain.Orders;
using Refit;

namespace OrderManager.ApplicationCore.Orders;

public interface IOrderAPI {

    [Get($"/{nameof(GetOrders)}")]
    Task<IEnumerable<OrderSummary>> GetOrders();

    [Get("/Orders/{OrderId}")]
    Task<OrderDetails> GetOrderDetails([AliasAs("OrderId")] int orderId);

    [Put("/Orders/{OrderId}/items/{ItemId}/AddItemToOrder")]
    Task AddItemToOrder([AliasAs("OrderId")] int orderId, [AliasAs("ItemId")] int itemId, [Body] AddItemOrderCommand command);

    public class AddItemOrderCommand {
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public string Options { get; set; } = string.Empty;
    }

    [Put("/Orders/items/{ItemId}/SetOptionValue")]
    Task SetOptionValue([AliasAs("ItemId")] int itemId,[Body] SetOptionValueCommand command);

    public class SetOptionValueCommand {
        public string Option { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    [Put("/Orders/items/{ItemId}/SetQty}")]
    Task SetQty([AliasAs("ItemId")] int itemId, [Body] SetQtyCommand command);

    public class SetQtyCommand {
        public int Qty {get; set;}
    }

    [Put("/Orders/PlaceOrder)")]
    Task<int> PlaceOrder();

    public class PlaceOrderCommand {
        public string Name { get; set; } = string.Empty;
        public string Number { get; set; } = string.Empty;
        public int CustomerId { get; set; }
        public int VendorId { get; set; }
        public int SupplierId { get; set; }
        public string Fields { get; set; } = string.Empty;
    }

    [Put("/Orders/{id}/ConfirmOrder}")]
    Task ConfirmOrder([AliasAs("id")] int orderId);

    [Put("/Orders/{id}/VoidOrder}")]
    Task VoidOrder([AliasAs("id")] int orderId);

}