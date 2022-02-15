using Domain.Entities;
using Domain.Entities.OrderAggregate;
using Persistance.Repositories.Companies;
using Persistance.Repositories.OrderItems;
using Persistance.Repositories.Orders;

namespace Domain.Services;

public class OrderService : EntityService {

    private readonly IOrderRepository _orderRepository;
    private readonly IOrderItemRepository _itemRepository;
    private readonly ICompanyRepository _companyRepository;

    public OrderService(OrderRepository orderRepository, OrderItemRepository itemRepository, CompanyRepository companyRepository) {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _companyRepository = companyRepository;
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

        var order = new Order() {
            Id = orderDao.Id,
            Number = orderDao.Number,
            Name = orderDao.Name,
            IsPriority = orderDao.IsPriority,
            LastModified = orderDao.LastModified,
        };

        foreach (var item in items) {
            // Map the OrderItemDAO objects to  CatalogItemOrdered objects
            order.AddItem(new(item.ProductId, item.ProductName, new List<string>()), item.Qty);
        }

        order.Customer = GetCompany(orderDao.CustomerId);
        order.Vendor = GetCompany(orderDao.VendorId);
        order.Supplier = GetCompany(orderDao.SupplierId);

        return order;
    }

    private Company GetCompany(int id) {
        var dao = _companyRepository.GetCompanyById(id);
        return new() {
            Id = dao.Id,
            Name = dao.Name
        };
    }

    /// <summary>
    /// Updates the order on the database and all of the items in the order
    /// </summary>
    public void UpdateOrder(Order order) {

        _orderRepository.UpdateOrder(new() {
            Id = order.Id,
            Number = order.Number,
            Name = order.Name,
            IsPriority = order.IsPriority,
            LastModified = order.LastModified,
            CustomerId = order.Customer?.Id ?? -1,
            VendorId = order.Vendor?.Id ?? -1,
            SupplierId = order.Supplier?.Id ?? -1,
        });

        foreach (var item in order.Items) {
            _itemRepository.UpdateItem(new() {
                Id = item.Id,
                OrderId = item.OrderId,
                ProductId = item.OrderedItem.ProductId,
                ProductName = item.OrderedItem.ProductName,
                Qty = item.Qty.Value,
            });
        }

        if (order.Customer is not null)
            UpdateCompany(order.Customer);
        if (order.Vendor is not null)
            UpdateCompany(order.Vendor);
        if (order.Supplier is not null)
            UpdateCompany(order.Supplier);

    }

    private void UpdateCompany(Company company) {
        _companyRepository.UpdateCompany(new() {
            Id = company.Id,
            Name = company.Name,
        });
    }


}
