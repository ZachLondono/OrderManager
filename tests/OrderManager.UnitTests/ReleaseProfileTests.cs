using OrderManager.Features.Ribbon.ReleaseProfiles;
using System;
using Xunit;

namespace OrderManager.UnitTests;

public class ReleaseProfileTests {

    [Fact]
    public void Should_UpdateName() {

        // Arrange
        ReleaseProfile rp = new(Guid.NewGuid(), "Profile A", new());
        ReleaseProfileEventDomain rped = new(rp);

        string newName = "Profile B";

        // Act
        rped.ChangeProfileName(newName);

        // Assert
        var events = rped.GetEvents();
        Assert.Contains(new ChangeProfileNameEvent(newName), events);

    }

    [Fact]
    public void Should_AddNewAction() {
        
        // Arrange
        ReleaseProfile rp = new(Guid.NewGuid(), "Profile A", new());
        ReleaseProfileEventDomain rped = new(rp);

        // Act
        rped.AddAction("Action A");
        rped.AddAction("Action B");
        rped.AddAction("Action C");
        rped.AddAction("Action D");

        // Assert
        var events = rped.GetEvents();
        Assert.Contains(new AddActionEvent(0, "Action A"), events);
        Assert.Contains(new AddActionEvent(1, "Action B"), events);
        Assert.Contains(new AddActionEvent(2, "Action C"), events);
        Assert.Contains(new AddActionEvent(3, "Action D"), events);

    }

    [Fact]
    public void Should_RemoveAction() {
        
        // Arrange
        ReleaseProfile rp = new(Guid.NewGuid(), "Profile A", new());
        ReleaseProfileEventDomain rped = new(rp);
        rped.AddAction("Action A");
        rped.AddAction("Action B");
        rped.AddAction("Action C");
        rped.AddAction("Action D");

        // Act
        rped.RemoveAction("Action B");

        // Assert
        var events = rped.GetEvents();
        Assert.Contains(new RemoveActionEvent("Action B"), events);

    }

    [Fact]
    public void Should_MoveAction_FromMiddle() {

        // Arrange
        ReleaseProfile rp = new(Guid.NewGuid(), "Profile A", new());
        ReleaseProfileEventDomain rped = new(rp);
        rped.AddAction("Action A"); // 0
        rped.AddAction("Action B"); // 1
        rped.AddAction("Action C"); // 2
        rped.AddAction("Action D"); // 3

        // Act
        rped.MoveActionToPosition("Action B", 3);

        // Assert
        var events = rped.GetEvents();
        Assert.Contains(new MoveActionEvent(3, "Action B"), events);
        Assert.Contains(new MoveActionEvent(2, "Action D"), events);
        Assert.Contains(new MoveActionEvent(1, "Action C"), events);

    }

    [Fact]
    public void Should_MoveAction_FromEnd() {

        // Arrange
        ReleaseProfile rp = new(Guid.NewGuid(), "Profile A", new());
        ReleaseProfileEventDomain rped = new(rp);
        rped.AddAction("Action A"); // 0
        rped.AddAction("Action B"); // 1
        rped.AddAction("Action C"); // 2
        rped.AddAction("Action D"); // 3

        // Act
        rped.MoveActionToPosition("Action D", 0);

        // Assert
        var events = rped.GetEvents();
        Assert.Contains(new MoveActionEvent(0, "Action D"), events);
        Assert.Contains(new MoveActionEvent(1, "Action A"), events);
        Assert.Contains(new MoveActionEvent(2, "Action B"), events);
        Assert.Contains(new MoveActionEvent(3, "Action C"), events);

    }

    [Fact]
    public void Should_MoveAction_FromBeginning() {

        // Arrange
        ReleaseProfile rp = new(Guid.NewGuid(), "Profile A", new());
        ReleaseProfileEventDomain rped = new(rp);
        rped.AddAction("Action A"); // 0
        rped.AddAction("Action B"); // 1
        rped.AddAction("Action C"); // 2
        rped.AddAction("Action D"); // 3

        // Act
        rped.MoveActionToPosition("Action A", 3);

        // Assert
        var events = rped.GetEvents();
        Assert.Contains(new MoveActionEvent(3, "Action A"), events);
        Assert.Contains(new MoveActionEvent(0, "Action B"), events);
        Assert.Contains(new MoveActionEvent(1, "Action C"), events);
        Assert.Contains(new MoveActionEvent(2, "Action D"), events);

    }

}
