using Domain.Entities;
using System.Linq;
using Xunit;

namespace Domain.UnitTests;

public class OrderTests {

    [Fact]
    public void Should_AddItem_ToOrder() {

        // Arrange
        Order order = new Order();
        CatalogProduct product = new CatalogProduct();
        product.Name = "Test Product";

        int qty = 1;

        // Act
        var exception = Record.Exception(() => order.AddItem(product, qty));

        // Assert
        Assert.Null(exception);
        Assert.Single(order.Items);
        Assert.Equal(1, order.Items.Sum(p => p.Qty));

    }

    [Fact]
    public void Should_NotAddItem_ToOrder() {

        // Arrange
        Order order = new Order();
        CatalogProduct product = new CatalogProduct();
        product.Name = "Test Product";

        int qty = -1;

        // Act
        var exception = Record.Exception(() => order.AddItem(product, qty));


        // Assert
        Assert.NotNull(exception);

    }

}
