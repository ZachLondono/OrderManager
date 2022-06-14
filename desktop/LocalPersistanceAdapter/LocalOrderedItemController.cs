using OrderManager.ApplicationCore.Orders;
using Refit;

namespace LocalPersistanceAdapter;

public class LocalOrderedItemController : IOrderedItemController {

    public Task AddItemToOrder([Body(true)] IOrderedItemController.AddItemToOrderCommand command) {
        throw new NotImplementedException();
    }

    public Task GetOrderedItemDetails(int orderId) {
        throw new NotImplementedException();
    }

    public Task RemoveItemFromOrder(int itemId) {
        throw new NotImplementedException();
    }

    public Task SetOptionValue([Body(true)] IOrderedItemController.SetOptionValueCommnad command) {
        throw new NotImplementedException();
    }

    public Task SetQty([Body(true)] IOrderedItemController.SetQtyCommnad commnad) {
        throw new NotImplementedException();
    }

}