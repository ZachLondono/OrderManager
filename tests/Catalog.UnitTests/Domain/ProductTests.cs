using Catalog.Implementation.Domain;
using FluentAssertions;
using System;
using Xunit;

namespace Catalog.UnitTests.Domain;

public class ProductTests {

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("   ")]
    [InlineData("       ")]
    public void ShouldNotCreateProduct_WithInvalidName(string name) {
        var createProduct = () => new Product(name, Guid.NewGuid());
        createProduct.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("   ")]
    [InlineData("       ")]
    public void ShouldNotAddAttribute_WithInvalidName(string name) {
        var addAttribute = () => new Product("Product", Guid.NewGuid()).AddAttribute(name);
        addAttribute.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldNotAddDuplicateAttribute() {
        var p = new Product("Product", Guid.NewGuid());
        
        string attributeName = "Attr";
        p.AddAttribute(attributeName);

        var addAttribute = () => p.AddAttribute(attributeName);
        addAttribute.Should().Throw<ArgumentException>();
    }
}
