using Xunit;
using System.Collections.Generic;
using OrderManager.ApplicationCore.Labels;
using OrderManager.Domain.Orders;

namespace ApplicationCore.UnitTests;

public class LabelServiceTests {

    [Fact]
    public void Should_Fill_OrderLabel_Fields() {

        // Arrange
        string orderName = "OrderName";
        string orderNumber = "OrderNumber";
        Order order = new(0, orderName, orderNumber, new List<LineItem>());
        Dictionary<string, string> fieldFormulas = new() {
            { "Field1", "{order.Name}" },
            { "Field2", "{order.Number}" }
        };

        // Act
        Dictionary<string, string> filledFields = LabelService.EvaluateOrderLabelFormulas(order, fieldFormulas).Result;

        // Assert
        Assert.Equal(orderName, filledFields["Field1"]);
        Assert.Equal(orderNumber, filledFields["Field2"]);

    }

    [Fact]
    public void Should_Fill_ProductLabel_Fields() {

        // Arrange
        string orderName = "OrderName";
        string orderNumber = "OrderNumber";
        string productName = "ProductName";
        Order order = new(0, orderName, orderNumber, new List<LineItem>());
        LineItem item = new(0, productName, 1, 1, new());
        Dictionary<string, string> fieldFormulas = new() {
            { "Field1", "{order.Name}" },
            { "Field2", "{order.Number}" },
            { "Field3", "{item.ProductName}" }
        };

        // Act
        Dictionary<string, string> filledFields = LabelService.EvaluateProductLabelFormulas(order, item, fieldFormulas).Result;

        // Assert
        Assert.Equal(orderName, filledFields["Field1"]);
        Assert.Equal(orderNumber, filledFields["Field2"]);
        Assert.Equal(productName, filledFields["Field3"]);

    }


}