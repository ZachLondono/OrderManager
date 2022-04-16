using Xunit;
using OrderManager.ApplicationCore.Common;
using OrderManager.Domain.Labels;

namespace ApplicationCore.UnitTests;

public class FormulaServiceTests {

    [Theory]
    [InlineData("Hello World", "Hello World")]
    [InlineData("Sum {1 + 2}", "Sum 3")]
    [InlineData("{data}", "Hello World")]
    public void Should_Execute_1ArgFormula(string input, string expected) {

        var output = FormulaService.ExecuteFormula(input, "Hello World", "data").Result;
        Assert.Equal(expected, output);

    }

    [Theory]
    [InlineData("Hello World", "Hello World")]
    [InlineData("Sum {1 + 2}", "Sum 3")]
    [InlineData("{data1 + data2}", "3")]
    public void Should_Execute_2ArgFormula(string input, string expected) {
        var output = FormulaService.ExecuteFormula(input, 1, 2, "data1", "data2").Result;
        Assert.Equal(expected, output);
    }

}