using Xunit;
using OrderManager.ApplicationCore.Services;
using System.Threading.Tasks;

namespace ApplicationCore.UnitTests;

public class FormulaServiceTests {

    [Theory]
    [InlineData("Hello World", "Hello World")]
    [InlineData("Sum {1 + 2}", "Sum 3")]
    [InlineData("{data.Arg1} {data.Arg2}", "Hello World")]
    public void Should_Execute_1ArgFormula(string input, string expected) {

        var output = FormulaService.ExecuteFormula<object>(input, new { Arg1 = "Hello", Arg2 = "World" }, "data").Result;
        Assert.Equal(expected, output);

    }

    [Theory]
    [InlineData("Hello World", "Hello World")]
    [InlineData("Sum {1 + 2}", "Sum 3")]
    [InlineData("{data1.Arg1} {data2.Arg2}", "Hello World")]
    public void Should_Execute_2ArgFormula(string input, string expected) {
        var output = FormulaService.ExecuteFormula<object, object>(input, new { Arg1 = "Hello" }, new { Arg2 = "World" }, "data1", "data2").Result;
        Assert.Equal(expected, output);
    }

}
