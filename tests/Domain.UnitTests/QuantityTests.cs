using Domain.Entities.ValueObjects;
using Xunit;

namespace Domain.UnitTests;

public class QuantityTests {


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(100)]
    public void Should_CreateQuantity(int qty) {

        // Act
        var exception = Record.Exception(() => new Quantity(qty));

        // Assert
        Assert.Null(exception);

    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-100)]
    public void Should_Not_CreateQuantity(int qty) {

        // Act
        var exception = Record.Exception(() => new Quantity(qty));

        // Assert
        Assert.NotNull(exception);

    }

    [Fact]
    public void Should_SumQuantities() {

        // Arrange
        Quantity q1 = new(1);
        Quantity q2 = new(2);

        // Act
        Quantity sum = q1 + q2;

        // Assert
        Assert.Equal(3, sum.Value);

    }

    [Fact]
    public void Should_SubtractQuantities() {

        // Arrange
        Quantity q1 = new(2);
        Quantity q2 = new(1);

        // Act
        Quantity difference = q1 - q2;

        // Assert
        Assert.Equal(1, difference.Value);

    }

    [Fact]
    public void Should_MultiplyQuantities() {

        // Arrange
        Quantity q1 = new(2);
        Quantity q2 = new(1);

        // Act
        Quantity product = q1 * q2;

        // Assert
        Assert.Equal(2, product.Value);

    }


    [Fact]
    public void Should_DivideQuantities() {

        // Arrange
        Quantity q1 = new(2);
        Quantity q2 = new(1);

        // Act
        Quantity quotient = q1 / q2;

        // Assert
        Assert.Equal(2, quotient.Value);

    }

}
