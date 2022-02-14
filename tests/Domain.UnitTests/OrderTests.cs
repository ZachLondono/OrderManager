using Domain.Entities.OrderAggregate;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Domain.UnitTests;

public class OrderTests {

    [Fact]
    public void Should_AddItem_ToOrder() {

        // Arrange
        Order order = new Order();
        string productName = "Test Product";
        CatalogItemOrdered item = new(0, productName, new List<string>() { });

        int qty = 1;

        // Act
        var exception = Record.Exception(() => order.AddItem(item, qty));

        // Assert
        Assert.Null(exception);
        Assert.Single(order.Items);
        Assert.Equal(1, order.Items.Sum(p => p.Qty));

    }

    [Fact]
    public void Should_NotAddItem_ToOrder() {

        // Arrange
        Order order = new Order();
        string productName = "Test Product";
        CatalogItemOrdered item = new(0, productName, new List<string>() { });

        int qty = -1;

        // Act
        var exception = Record.Exception(() => order.AddItem(item, qty));


        // Assert
        Assert.NotNull(exception);

    }

}
