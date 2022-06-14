using OrderManager.Domain.Orders;
using Refit;

namespace OrderManager.ApplicationCore.Orders;

public interface IOrderController {

    [Get($"/Orders")]
    Task<IEnumerable<OrderSummaryResponse>> GetOrders();

    [Get("/Orders/{OrderId}")]
    Task<OrderDetailsResponse> GetOrderDetails([AliasAs("OrderId")] int orderId);

    [Get("/Orders/{OrderId}/Items")]
    Task GetOrderedItemsDetails([AliasAs("OrderId")] int orderId);

    [Post("/Orders/Items")]
    Task AddItemToOrder([Body(buffered: true)] AddItemOrderCommand command);

    public class AddItemOrderCommand {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Qty { get; set; }
        public string Options { get; set; } = string.Empty;
    }

    [Put("/Orders/Items/SetOptionValue")]
    Task SetOptionValue([Body(buffered: true)] SetOptionValueCommand command);

    public class SetOptionValueCommand {
        public int ItemId { get; set; }
        public string Option { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    [Put("/Orders/Items/SetQty}")]
    Task SetQty([Body(buffered: true)] SetQtyCommand command);

    public class SetQtyCommand {
        public int ItemId { get; set; }
        public int Qty {get; set;}
    }

    [Post("/Orders/)")]
    Task<int> PlaceOrder([Body(buffered: true)] PlaceOrderCommand command);

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