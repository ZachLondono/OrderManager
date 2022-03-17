using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace OrderManager.Features.OrderDetails;

public class OrderDetailsRepository {

    private readonly IDbConnection _connection;
    private readonly ILogger<OrderDetailsRepository> _logger;

    public OrderDetailsRepository(IDbConnection connection, ILogger<OrderDetailsRepository> logger) {
        _connection = connection;
        _logger = logger;
    }

    public OrderDetailsEventDomain GetOrderById(Guid id) {

        const string orderQuery = "SELECT * FROM Orders WHERE Id = @Id;";
        const string companyQuery = "SELECT * FROM Companies WHERE Id = @CompanyId;";
        const string orderItemQuery = "SELECT * FROM OrderItems WHERE OrderId = @Id;";
        const string itemOptionsQuery = "SELECT * FROM OrderItemOptions WHERE ItemId = @ItemId;";

        var details = _connection.QuerySingle<OrderDetails>(orderQuery, new { Id = id.ToString() });
        details.Customer = _connection.QuerySingle<CompanyDetails>(companyQuery, new { CompanyId = details.CustomerId });
        details.Vendor = _connection.QuerySingle<CompanyDetails>(companyQuery, new { CompanyId = details.VendorId });
        details.Supplier = _connection.QuerySingle<CompanyDetails>(companyQuery, new { CompanyId = details.SupplierId });

        details.OrderedProducts = _connection.Query<OrderedProduct>(orderItemQuery, new { Id = id.ToString() });
        foreach (var product in details.OrderedProducts) {
            product.Options.AddRange(_connection.Query<ProductOption>(itemOptionsQuery, new { ItemId = product.Id }));
        }

        return new(details);

    }

    public void Save(OrderDetailsEventDomain orderDetailsEvent) {

        _connection.Open();
        var trx = _connection.BeginTransaction();

        var commentUpdate = orderDetailsEvent.GetEvents().Where(e => e is OrderCommentEditedEvent).LastOrDefault();
        if (commentUpdate is not null) {
            EditOrderComment((OrderCommentEditedEvent) commentUpdate, trx);
        }

        foreach (var e in orderDetailsEvent.GetEvents()) {

            if (e is ProductAttributeEditedEvent productEdit) {
                EditProductAttribute(productEdit, trx);
            }

        }

        trx.Commit();
        _connection.Close();

    }

    private void EditOrderComment(OrderCommentEditedEvent e, IDbTransaction trx) {
        const string command = "UPDATE [Orders] SET [Comment] = @Comment WHERE [Id] = @OrderId";
        int rows = _connection.Execute(command, new {
            e.Comment,
            OrderId = e.OrderId.ToString()
        }, trx);
        _logger.LogTrace("Order comment updated, rows {rows}", rows);
    }

    private void EditProductAttribute(ProductAttributeEditedEvent e, IDbTransaction trx) {
        const string command = "UPDATE [OrderItemOptions] SET [Value] = @Value WHERE [ItemId] = @ItemId AND [Key] = @Attribute;";
        _connection.Execute(command, e, trx);
    }

}

public class OrderDetailsEventDomain {

    private readonly OrderDetails _order;
    private readonly List<object> _events;

    public OrderDetailsEventDomain(OrderDetails order) {
        _order = order;
        _events = new List<object>();
    }

    public IEnumerable<object> GetEvents() => _events;

    public string Comment{

        get => _order.Comment;

        set { 
            _events.Add(new OrderCommentEditedEvent(_order.Id, value));
            _order.Comment = value;
        }
    }

    public void SetProductAttribute(int productId, string attribute, string value) {

        OrderedProduct? product = _order.OrderedProducts
                                        .Where(p => p.Id == productId)
                                        .FirstOrDefault();

        if (product is null) return;

        ProductOption? option = product.Options
                                        .Where(o => o.Key == attribute)
                                        .FirstOrDefault();

        if (option is null) return;

        option.Value = value;

        _events.Add(new ProductAttributeEditedEvent(productId, attribute, value));

    }

}

public record OrderCommentEditedEvent(Guid OrderId, string Comment);

public record ProductAttributeEditedEvent(int ItemId, string Attribute, string Value);


public class OrderedProductEventDomain {

    private readonly OrderedProduct _product;

    public OrderedProductEventDomain(OrderedProduct product) {
        _product = product;
    }

    public int Qty => _product.Qty;

    public int LineNumber => _product.LineNumber;

    public IReadOnlyCollection<ProductOption> Options => _product.Options.AsReadOnly();

    public string? this[string attribute] {

        get => _product.Options
                            .Where(o => o.Key == attribute)
                            .Select(o => o.Value)
                            .FirstOrDefault();
        
        set {
            var option = _product.Options
                            .Where(o => o.Key == attribute)
                            .FirstOrDefault();

            if (option is not null)
                option.Value = value ?? "";
            else {
                _product.Options.Add(new() {
                    Key = attribute,
                    Value = value ?? ""
                });
            }
        }

    }

}