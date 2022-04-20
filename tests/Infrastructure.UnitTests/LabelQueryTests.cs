using Dapper;
using Infrastructure.Labels;
using Infrastructure.Labels.Queries;
using Microsoft.Data.Sqlite;
using OrderManager.Domain.Labels;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using Xunit;

namespace Infrastructure.UnitTests;

public class LabelQueryTests {

    private readonly IDbConnection _connection;

    public LabelQueryTests() {
        _connection = new SqliteConnection("Data Source=:memory:;");
    }

    private void SetupSchema() {
        _connection.Execute(File.ReadAllText("Schema.sql"));
    }

    [Fact]
    public void Should_Create_And_Read_Label() {

        // Arrange
        var repo = new LabelFieldMapRepository(_connection);
        var query = new GetLabelByIdQuery(_connection);
        string name = "New Name";
        string path = @"c:/path/to/file";
        LabelType type = LabelType.Order;
        Dictionary<string, string> fields = new() {
            { "KeyA", "ValueA" }
        };

        // Act
        _connection.Open();

        SetupSchema();

        var context = repo.Add(name, path, type, fields).Result;

        var label = query.GetLabelById(context.Id).Result;

        _connection.Close();

        // Assert
        Assert.Equal(name, label?.Name);
        Assert.Equal(path, label?.TemplatePath);
        Assert.Equal(type, label?.Type);
        Assert.Equal(fields, label?.Fields);

    }

    [Fact]
    public void Should_Create_And_Read_LabelSummaries() {

        // Arrange
        var repo = new LabelFieldMapRepository(_connection);
        var query = new GetLabelSummariesQuery(_connection);
        string name1 = "New Name1";
        string name2 = "New Name2";
        string path = @"c:/path/to/file";
        LabelType type = LabelType.Order;
        Dictionary<string, string> fields = new() { { "KeyA", "ValueA" } };

        // Act
        _connection.Open();

        SetupSchema();

        _ = repo.Add(name1, path, type, fields).Result;
        _ = repo.Add(name2, path, type, fields).Result;

        var labels = query.GetLabelSummaries().Result;

        _connection.Close();

        // Assert
        Assert.Contains(labels, l => l.Name == name1);
        Assert.Contains(labels, l => l.Name == name2);
    }

    [Fact]
    public void Should_Create_And_Remove_Labels() {

        // Arrange
        var repo = new LabelFieldMapRepository(_connection);
        var query = new GetLabelSummariesQuery(_connection);
        string name1 = "New Name1";
        string name2 = "New Name2";
        string path = @"c:/path/to/file";
        LabelType type = LabelType.Order;
        Dictionary<string, string> fields = new() { { "KeyA", "ValueA" } };

        // Act
        _connection.Open();

        SetupSchema();

        var c1 = repo.Add(name1, path, type, fields).Result;
        var c2 = repo.Add(name2, path, type, fields).Result;

        repo.Remove(c1.Id).Wait();
        repo.Remove(c2.Id).Wait();

        var labels = query.GetLabelSummaries().Result;

        _connection.Close();

        // Assert
        Assert.Empty(labels);
    }

    [Fact]
    public void Should_Create_Label() {
        
        // Arrange
        var repo = new LabelFieldMapRepository(_connection);

        // Act
        _connection.Open();
        SetupSchema();
        var result  = repo.Add("name", "path", LabelType.Order, new Dictionary<string, string>()).Result;
        _connection.Close();

        // Assert
        Assert.True(result.Id > 0);

    }

    [Fact]
    public void Should_Create_Edit_Read_Label() {

        // Arrange
        var repo = new LabelFieldMapRepository(_connection);
        var query = new GetLabelDetailsByIdQuery(_connection);
        string newName = "New Name";

        // Act
        _connection.Open();
        SetupSchema();

        var context = repo.Add("name", "path", LabelType.Order, new Dictionary<string, string>()).Result;

        context.SetName(newName);

        repo.Save(context).Wait();

        var updated = query.GetLabelDetailsById(context.Id).Result;

        _connection.Close();

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(newName, updated?.Name);

    }

}
