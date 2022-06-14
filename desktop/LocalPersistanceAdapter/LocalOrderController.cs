using OrderManager.ApplicationCore.Orders;
using OrderManager.Domain.Orders;
using Refit;

namespace LocalPersistanceAdapter;

public class LocalOrderController : IOrderController {

    public Task AddItemToOrder([Body(true)] IOrderController.AddItemOrderCommand command) {
        throw new NotImplementedException();
    }

    public Task ConfirmOrder([AliasAs("id")] int orderId) {
        throw new NotImplementedException();
    }

    public Task<OrderDetailsResponse> GetOrderDetails([AliasAs("OrderId")] int orderId) {
        throw new NotImplementedException();
    }

    public Task GetOrderedItemsDetails([AliasAs("OrderId")] int orderId) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderSummaryResponse>> GetOrders() {
        throw new NotImplementedException();
    }

    public Task<int> PlaceOrder([Body(true)] IOrderController.PlaceOrderCommand command) {
        throw new NotImplementedException();
    }

    public Task SetOptionValue([Body(true)] IOrderController.SetOptionValueCommand command) {
        throw new NotImplementedException();
    }

    public Task SetQty([Body(true)] IOrderController.SetQtyCommand command) {
        throw new NotImplementedException();
    }

    public Task VoidOrder([AliasAs("id")] int orderId) {
        throw new NotImplementedException();
    }

}