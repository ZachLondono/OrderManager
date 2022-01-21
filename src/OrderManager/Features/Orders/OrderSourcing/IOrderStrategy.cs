using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Orders.OrderSourcing;

public interface IOrderLoadStrategy {
    public Order LoadOrder();
}

public class RESTOrderStrategy : IOrderLoadStrategy {
    public Order LoadOrder() {
        // Connects to a REST api and returns a refit http client
        throw new NotImplementedException();
    }
}