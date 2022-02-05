using OrderManager.ApplicationCore.Features.Triggers;
using OrderManager.ApplicationCore.Infrastructure.Attributes;
using System.Collections.Generic;
using Xunit;

namespace OrderManager.ApplicationCore.Unit.Tests;

public class TriggerControllerTests {
    
    [ExpandableModel]
    private class TestModel {

        [ExpandableProperty]
        public IDictionary<string, object>? Attributes { get; set; }

    }
    
    [Fact]
    public void Should_Have_Properties_Present() {

        // Arrange 
        TestModel baseModel = new() {
            Attributes = new Dictionary<string, object>() {
                { "AttributeA", "ValueA" },
                { "AttributeB", "ValueB" }
            }
        };

        // Act
        dynamic model = TriggerController.GenerateExpandedModel(baseModel);


        // Assert
        var properties = model as IDictionary<string, object>;

        Assert.NotNull(properties);

        Assert.True(properties.ContainsKey("AttributeA"));
        Assert.Equal("ValueA", properties["AttributeA"]);

        Assert.True(properties.ContainsKey("AttributeB"));
        Assert.Equal("ValueB", properties["AttributeB"]);

    }

}