using Domain.Entities;
using Persistance.Repositories.Catalog;
using Persistance.Repositories.OrderItems;
using Persistance.Repositories.Orders;

namespace Domain.Services;

public class OrderService : IEntityService {

    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _itemRepository;
    private readonly ICatalogRepository _catalogRepository;

    public OrderService(IOrderRepository orderRepository, IOrderItemRepository itemRepository, ICatalogRepository catalogRepository) {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _catalogRepository = catalogRepository;
    }

    public IEnumerable<Order> GetAllOrders() {
        IEnumerable<OrderDAO> daos = _orderRepository.GetOrders();
        List<Order> orders = new();
        foreach (var dao in daos) {
            orders.Add(GetOrder(dao));
        }
        return orders;
    }

    /// <summary>
    /// Gets an order and all its order items from the database
    /// </summary>
    public Order GetOrderById(int id) {
        OrderDAO orderDao = _orderRepository.GetOrderById(id);
        return GetOrder(orderDao);
    }

    /// <summary>
    /// Gets a Domain Order entity from a Persistance OrderDAO entity
    /// </summary>
    private Order GetOrder(OrderDAO orderDao) {
        
        IEnumerable<OrderItemDAO> items = _itemRepository.GetItemsByOrderId(orderDao.Id);
        IEnumerable<CatalogProductDAO> catalog = _catalogRepository.GetCatalog();

        var order = new Order() {
            Id = orderDao.Id,
            Number = orderDao.Number
        };

        foreach (var item in items) {
            CatalogProductDAO? product = catalog.Where(x => x.Id == item.ProductId).FirstOrDefault();
            if (product is null) continue;

            order.AddItem(new() {
                Id = product.Id,
            }, item.Qty);
        }

        return order;
    }

    /// <summary>
    /// Updates the order on the database and all of the items in the order
    /// </summary>
    public void UpdateOrder(Order order) {

        _orderRepository.UpdateOrder(new() {
            Id = order.Id,
            Number = order.Number
        });

        foreach (var item in order.Items) {
            _itemRepository.UpdateItem(new() {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.ProductId,
                Qty = item.Qty
            });
        }

    }

}
