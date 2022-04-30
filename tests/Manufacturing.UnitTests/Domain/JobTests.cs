using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Manufacturing.Implementation.Domain;

namespace Manufacturing.UnitTests.Domain;

public class JobTests {

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("   ")]
    [InlineData("       ")]
    public void ShouldNotCreateJob_WithInvalidName(string name) {
        var addAttribute = () => new Job(0, name, "number", 0, 0, 0);
        addAttribute.Should().Throw<ArgumentException>();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("  ")]
    [InlineData("   ")]
    [InlineData("       ")]
    public void ShouldNotCreateJob_WithInvalidNumber(string number) {
        var addAttribute = () => new Job(0, "name", number, 0, 0, 0);
        addAttribute.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ShouldReleaseJob() {
        var j = new Job(0, "name", "number", 0, 0, 1);
        j.ReleaseToProduction();
        j.ReleasedDate
            .Should()
            .NotBeNull()
            .And
            .BeBefore(DateTime.Now);
        j.Status
            .Should()
            .Be(ManufacturingStatus.InProgress);
    }

    [Fact]
    public void ShouldCompleteJob() {
        var j = new Job(0,"name", "number", 0, 0, 1);
        j.Complete();
        j.ReleasedDate
            .Should()
            .NotBeNull()
            .And
            .BeBefore(DateTime.Now);
        j.CompletedDate
            .Should()
            .NotBeNull()
            .And
            .BeBefore(DateTime.Now);
        j.Status
            .Should()
            .Be(ManufacturingStatus.Completed);
    }

    [Fact]
    public void ShouldShipJob() {
        var j = new Job(0, "name", "number", 0, 0, 1);
        j.Ship();
        j.ReleasedDate
            .Should()
            .NotBeNull()
            .And
            .BeBefore(DateTime.Now);
        j.CompletedDate
            .Should()
            .NotBeNull()
            .And
            .BeBefore(DateTime.Now);
        j.ShippedDate
            .Should()
            .NotBeNull()
            .And
            .BeBefore(DateTime.Now);
        j.Status
            .Should()
            .Be(ManufacturingStatus.Shipped);
    }

}
