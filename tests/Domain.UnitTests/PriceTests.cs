using Domain.Entities.ValueObjects;
using Xunit;

namespace Domain.UnitTests;

public class PriceTests {

    [Fact]
    public void Should_RoundPrice() {

        // Arrange
        decimal val = 2.252M;

        // Act
        Price price = new(val);

        // Assert
        Assert.Equal(2.25M, price.Value);

    }

    [Fact]
    public void Should_RoundPrice_WhenMultiplying() {

        // Arrange
        Price p1 = new(2.25M);
        Price p2 = new(2.25M);

        // Act
        Price product = p1 * p2;

        // Assert
        Assert.Equal(5.06M, product.Value);

    }

    [Fact]
    public void Should_RoundPrice_WhenDividing() {

        // Arrange
        Price p1 = new(1.25M);
        Price p2 = new(2.25M);

        // Act
        Price quotient = p1 / p2;

        // Assert
        Assert.Equal(0.56M, quotient.Value);

    }

}
