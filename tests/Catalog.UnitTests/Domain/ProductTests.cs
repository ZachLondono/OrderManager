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
        var createProduct = () => new Product(0, name);
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
        var addAttribute = () => new Product(0, "Product").AddAttribute(new ProductAttribute() {
            Name = name,
            Default = "default"
        });
        addAttribute.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldNotAddDuplicateAttribute() {
        var p = new Product(0, "Product");
        
        string attributeName = "Attr";
        p.AddAttribute(new ProductAttribute() {
            Name = attributeName,
            Default = "default"
        });

        var addAttribute = () => p.AddAttribute(new ProductAttribute() {
            Name = attributeName,
            Default = "default"
        });
        addAttribute.Should().Throw<ArgumentException>();
    }
}
