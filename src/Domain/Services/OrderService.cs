using Domain.Entities;
using Persistance.Repositories.Catalog;
using Persistance.Repositories.Companies;
using Persistance.Repositories.OrderItems;
using Persistance.Repositories.Orders;

namespace Domain.Services;

public class OrderService : EntityService {

    private readonly OrderRepository _orderRepository;
    private readonly OrderItemRepository _itemRepository;
    private readonly CatalogRepository _catalogRepository;
    private readonly CompanyRepository _companyRepository;

    public OrderService(OrderRepository orderRepository, OrderItemRepository itemRepository, CatalogRepository catalogRepository, CompanyRepository companyRepository) {
        _orderRepository = orderRepository;
        _itemRepository = itemRepository;
        _catalogRepository = catalogRepository;
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
        IEnumerable<CatalogProductDAO> catalog = _catalogRepository.GetCatalog();

        var order = new Order() {
            Id = orderDao.Id,
            Number = orderDao.Number,
            Name = orderDao.Name,
            IsPriority = orderDao.IsPriority,
            LastModified = orderDao.LastModified,
        };

        foreach (var item in items) {
            CatalogProductDAO? product = catalog.Where(x => x.Id == item.ProductId).FirstOrDefault();
            if (product is null) continue;

            order.AddItem(new() {
                Id = product.Id,
            }, item.Qty);
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
                ProductId = item.ProductId,
                Qty = item.Qty,
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
