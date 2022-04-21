using Dapper;
using Infrastructure.Emails;
using Infrastructure.Emails.Queries;
using Microsoft.Data.Sqlite;
using OrderManager.ApplicationCore.Emails;
using OrderManager.Domain.Emails;
using System;
using System.Data;
using System.IO;
using Xunit;

namespace Infrastructure.UnitTests;

public class EmailQueryTests {

    private readonly IDbConnection _connection;

    public EmailQueryTests() {
        _connection = new SqliteConnection("Data Source=:memory:;");
    }

    private void SetupSchema() {
        _connection.Execute(File.ReadAllText("Schema.sql"));
    }


    [Fact]
    public void Should_Create_Email() {

        // Arrange
        var repo = new EmailTemplateRepository(_connection);

        // Act
        _connection.Open();
        SetupSchema();
        var result = repo.Add("New Email Template").Result;
        _connection.Close();

        // Assert
        Assert.True(result.Id > 0);

    }

    [Fact]
    public void Should_Create_And_Read_Email() {

        // Arrange
        var repo = new EmailTemplateRepository(_connection);

        // Act
        _connection.Open();
        SetupSchema();
        var result = repo.Add("New Email Template").Result;
        var context = repo.GetById(result.Id);
        _connection.Close();

        // Assert
        Assert.True(result.Id > 0);
        Assert.NotNull(context);

    }

    [Fact]
    public void Should_Create_EditName_Read_Email() {
        
        string newName = "Update Template Name";

        var result = Should_Create_Edit_Read_Email(a => a.SetName(newName));

        Assert.NotNull(result);
        Assert.Equal(newName, result?.Name);

    }

    [Fact]
    public void Should_Create_EditSubject_Read_Email() {

        string newSubject = "Update Template Subject";

        var result = Should_Create_Edit_Read_Email(a => a.SetSubject(newSubject));

        Assert.NotNull(result);
        Assert.Equal(newSubject, result?.Subject);

    }

    [Fact]
    public void Should_Create_EditBody_Read_Email() {

        string newBody = "Update Template Subject";

        var result = Should_Create_Edit_Read_Email(a => a.SetBody(newBody));

        Assert.NotNull(result);
        Assert.Equal(newBody, result?.Body);

    }

    [Fact]
    public void Should_Create_AddTo_Read_Email() {

        string newTo = "example@email.com";

        var result = Should_Create_Edit_Read_Email(a => a.AddTo(newTo));

        Assert.NotNull(result);
        Assert.Contains(newTo, result?.To);

    }

    [Fact]
    public void Should_Create_AddCc_Read_Email() {

        string newCC = "example@email.com";

        var result = Should_Create_Edit_Read_Email(a => a.AddCc(newCC));

        Assert.NotNull(result);
        Assert.Contains(newCC, result?.Cc);

    }

    [Fact]
    public void Should_Create_AddBcc_Read_Email() {

        string newBcc = "example@email.com";

        var result = Should_Create_Edit_Read_Email(a => a.AddBcc(newBcc));

        Assert.NotNull(result);
        Assert.Contains(newBcc, result?.Bcc);

    }

    [Fact]
    public void Should_Create_RemoveTo_Read_Email() {

        string newTo = "example@email.com";

        // Arrange
        var repo = new EmailTemplateRepository(_connection);
        var query = new GetEmailDetailsByIdQuery(_connection);

        // Act
        _connection.Open();

        SetupSchema();
        // create
        var result = repo.Add("New Email Template").Result;

        // edit
        result.AddTo(newTo);
        repo.Save(result).Wait();

        result.RemoveTo(newTo);
        repo.Save(result).Wait();

        // read
        var details = query.GetEmailDetailsById(result.Id).Result;

        _connection.Close();

        Assert.NotNull(details);
        Assert.Empty(details?.To);

    }

    [Fact]
    public void Should_Create_RemoveCc_Read_Email() {

        string newCc = "example@email.com";

        // Arrange
        var repo = new EmailTemplateRepository(_connection);
        var query = new GetEmailDetailsByIdQuery(_connection);

        // Act
        _connection.Open();

        SetupSchema();
        // create
        var result = repo.Add("New Email Template").Result;

        // edit
        result.AddCc(newCc);
        repo.Save(result).Wait();

        result.RemoveCc(newCc);
        repo.Save(result).Wait();

        // read
        var details = query.GetEmailDetailsById(result.Id).Result;

        _connection.Close();

        Assert.NotNull(details);
        Assert.Empty(details?.Cc);

    }

    [Fact]
    public void Should_Create_RemoveBcc_Read_Email() {

        string newBcc = "example@email.com";

        // Arrange
        var repo = new EmailTemplateRepository(_connection);
        var query = new GetEmailDetailsByIdQuery(_connection);

        // Act
        _connection.Open();

        SetupSchema();
        // create
        var result = repo.Add("New Email Template").Result;

        // edit
        result.AddBcc(newBcc);
        repo.Save(result).Wait();

        result.RemoveBcc(newBcc);
        repo.Save(result).Wait();

        // read
        var details = query.GetEmailDetailsById(result.Id).Result;

        _connection.Close();

        Assert.NotNull(details);
        Assert.Empty(details?.Bcc);

    }

    private EmailTemplateDetails? Should_Create_Edit_Read_Email(Action<EmailTemplateContext> action) {

        // Arrange
        var repo = new EmailTemplateRepository(_connection);
        var query = new GetEmailDetailsByIdQuery(_connection);

        // Act
        _connection.Open();
        
        SetupSchema();
        // create
        var result = repo.Add("New Email Template").Result;

        // edit
        action(result);
        repo.Save(result).Wait();

        // read
        var details = query.GetEmailDetailsById(result.Id).Result;

        _connection.Close();

        return details;

    }

    [Fact]
    public void Should_Create_Read_EmailSummeries() {

        // Arrange
        var repo = new EmailTemplateRepository(_connection);
        var query = new GetEmailSummariesQuery(_connection);
        string newTemplate1 = "New Template Name1";
        string newTemplate2 = "New Template Name2";

        // Act
        _connection.Open();

        SetupSchema();
        // create
        _ = repo.Add(newTemplate1).Result;
        _ = repo.Add(newTemplate2).Result;


        // read
        var templates = query.GetEmailSummaries().Result;

        _connection.Close();

        Assert.Contains(templates, t => t.Name.Equals(newTemplate1));
        Assert.Contains(templates, t => t.Name.Equals(newTemplate2));

    }

    [Fact]
    public void Should_Create_And_Read_EmailDetailsById() {

        // Arrange
        var repo = new EmailTemplateRepository(_connection);
        var query = new GetEmailDetailsByIdQuery(_connection);
        string newTemplate1 = "New Template Name1";

        // Act
        _connection.Open();
        SetupSchema();
        var result = repo.Add(newTemplate1).Result;
        var details = query.GetEmailDetailsById(result.Id).Result;
        _connection.Close();

        // Assert
        Assert.True(result.Id > 0);
        Assert.NotNull(details);
        Assert.Equal(newTemplate1, details.Name);

    }

}
