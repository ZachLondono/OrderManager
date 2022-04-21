using Dapper;
using Infrastructure.Profiles;
using Infrastructure.Profiles.Queries;
using Microsoft.Data.Sqlite;
using OrderManager.ApplicationCore.Profiles;
using OrderManager.Domain.Profiles;
using System;
using System.Data;
using System.IO;
using Xunit;

namespace Infrastructure.UnitTests;

public class ProfileQueryTests {

    private readonly IDbConnection _connection;

    public ProfileQueryTests() {
        _connection = new SqliteConnection("Data Source=:memory:;");
    }

    private void SetupSchema() {
        _connection.Execute(File.ReadAllText("Schema.sql"));
    }

    [Fact]
    public void Should_Create_Profile() {

        // Arrange
        var repo = new ReleaseProfileRepository(_connection);

        // Act
        _connection.Open();
        SetupSchema();
        var result = repo.Add("New Release Profile").Result;
        _connection.Close();

        // Assert
        Assert.True(result.Id > 0);

    }

    [Fact]
    public void Should_Create_And_Read_ProfileDetails() {

        // Arrange
        var repo = new ReleaseProfileRepository(_connection);
        var query = new GetProfileDetailsByIdQuery(_connection);
        string profileName = "New Release Profile";

        // Act
        _connection.Open();
        
        SetupSchema();

        var result = repo.Add(profileName).Result;

        var details = query.GetProfileDetailsById(result.Id).Result;

        _connection.Close();

        // Assert
        Assert.True(result.Id > 0);
        Assert.NotNull(details);
        Assert.Equal(profileName, details.Name);

    }


    [Fact]
    public void Should_Create_And_Read_ProfileSummaries() {

        // Arrange
        var repo = new ReleaseProfileRepository(_connection);
        var query = new GetProfileSummariesQuery(_connection);
        string profileName1 = "Profile1";
        string profileName2 = "Profile2";

        // Act
        _connection.Open();

        SetupSchema();

        _ = repo.Add(profileName1).Result;
        _ = repo.Add(profileName2).Result;

        var summaries = query.GetProfileSummaries().Result;

        _connection.Close();

        // Assert
        Assert.Contains(summaries, s => s.Name == profileName1);
        Assert.Contains(summaries, s => s.Name == profileName2);

    }

    [Fact]
    public void Should_Create_EditName_Read() {

        string newName = "Updated Name";

        var details = Should_Create_Edit_Read((p, r) => p.SetName(newName));

        Assert.Equal(newName, details.Name);

    }

    [Fact]
    public void Should_Create_AddPlugin_Read() {

        string pluginName = "PluginName";

        var details = Should_Create_Edit_Read((p, r) => p.AddPlugin(pluginName));

        Assert.Contains(pluginName, details.PluginNames);

    }

    [Fact]
    public void Should_Create_RemovePlugin_Read() {

        string pluginName = "PluginName";

        var details = Should_Create_Edit_Read((p, r) => {
            p.AddPlugin(pluginName);
            r.Save(p).Wait();
            p.RemovePlugin(pluginName);
        });

        Assert.Empty(details.PluginNames);

    }

    private ReleaseProfileDetails Should_Create_Edit_Read(Action<ReleaseProfileContext, ReleaseProfileRepository> action) {

        var repo = new ReleaseProfileRepository(_connection);
        var query = new GetProfileDetailsByIdQuery(_connection);
        string profileName = "New Release Profile";

        // Act
        _connection.Open();

        SetupSchema();

        var result = repo.Add(profileName).Result;

        action(result, repo);

        repo.Save(result).Wait();

        var details = query.GetProfileDetailsById(result.Id).Result;

        _connection.Close();

        return details;

    }

}
