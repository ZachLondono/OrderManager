using Domain.Entities.OrderAggregate;
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
        string productName = "Test Product";

        // Act
        OrderItem item = new(0, new(0, productName, attributes));

        // Assert
        foreach (var attribute in attributes) {
            Assert.True(item.OrderedItem.Options.ContainsKey(attribute));
        }

    }

    [Fact]
    public void Should_SetOptionValue() {
        
        // Arrange
        string attribute = "AttrA";
        string val = "ValA";
        string productName = "Test Product";
        OrderItem item = new(0, new(0, productName, new List<string>() { attribute }));

        // Act
        item.OrderedItem.SetOptionValue(attribute, val);

        // Assert
        Assert.Equal(val, item.OrderedItem.Options[attribute]);

    }

    [Fact]
    public void Should_Not_SetOptionValue() {

        // Arrange
        string attribute = "AttrA";
        string val = "ValA";
        string productName = "Test Product";
        OrderItem item = new(0, new(0, productName, new List<string>() { }));

        // Act
        item.OrderedItem.SetOptionValue(attribute, val);

        // Assert
        Assert.Throws<KeyNotFoundException>(() => item.OrderedItem.Options[attribute]);

    }

    [Fact]
    public void Should_Not_SetQuantity() {

        // Arrange
        string productName = "Test Product";
        OrderItem item = new(0, new(0, productName, new List<string>() { }));

        // Act
        var exception = Record.Exception(() => item.SetQty(-1));

        // Assert
        Assert.NotNull(exception);

    }

}
