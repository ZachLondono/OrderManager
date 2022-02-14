using Domain.Entities;
using System.Collections.Generic;
using Xunit;

namespace Domain.UnitTests;

public class OrderItemTests {

    [Fact]
    public void Should_ContainAttributes() {

        // Arrange
        List<string> attributes = new() {
            "AttrA",
            "AttrB",
            "AttrC"
        };
        CatalogProduct product = new CatalogProduct();
        product.Name = "Test Product";
        foreach (var attribute in attributes) {
            product.AddAttribute(attribute);
        }

        // Act
        OrderItem item = new(0, product);

        // Assert
        foreach (var attribute in attributes) {
            Assert.True(item.Options.ContainsKey(attribute));
        }

    }

    [Fact]
    public void Should_SetOptionValue() {
        
        // Arrange
        string attribute = "AttrA";
        string val = "ValA";
        CatalogProduct product = new CatalogProduct();
        product.AddAttribute(attribute);
        OrderItem item = new(0, product);

        // Act
        item.SetOptionValue(attribute, val);

        // Assert
        Assert.Equal(val, item.Options[attribute]);

    }

    [Fact]
    public void Should_Not_SetOptionValue() {

        // Arrange
        string attribute = "AttrA";
        string val = "ValA";
        CatalogProduct product = new CatalogProduct();
        OrderItem item = new(0, product);

        // Act
        item.SetOptionValue(attribute, val);

        // Assert
        Assert.Throws<KeyNotFoundException>(() => item.Options[attribute]);

    }

    [Fact]
    public void Should_Not_SetQuantity() {

        // Arrange
        CatalogProduct product = new();
        OrderItem item = new(0, product);

        // Act
        var exception = Record.Exception(() => item.SetQty(-1));

        // Assert
        Assert.NotNull(exception);

    }

}
