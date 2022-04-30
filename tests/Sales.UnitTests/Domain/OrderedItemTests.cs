using Sales.Implementation.Domain;
using System;
using FluentAssertions;
using Xunit;

namespace Sales.UnitTests.Domain;

public class OrderedItemTests {

    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    public void ShouldSetValidQty(int qty) {
        var item = new OrderedItem(0, 0, 0);
        var setQty = () => item.SetQuantity(qty);
        setQty.Should().NotThrow();
        item.Quantity.Should().Be(qty);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    public void ShouldNotSetInvalidQty(int qty) {
        var item = new OrderedItem(0, 0, 0);
        var setQty = () => item.SetQuantity(qty);
        setQty.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Theory]
    [InlineData("", "")]
    [InlineData(" ", "")]
    [InlineData("   ", "")]
    [InlineData("A", null)]
    public void ShouldNotSetOptionValue(string option, string value) {
        var setOption = () => new OrderedItem(0, 0, 0).SetOption(option, value);
        setOption.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldSetOptionValue() {
        var item = new OrderedItem(0, 0, 0);
        var setOption = () => item.SetOption("option", "value");
        setOption.Should().NotThrow();
        item.Options.Keys.Should().Contain("option");
        item.Options.Values.Should().Contain("value");
        item["option"].Should().Be("value");
    }

}
