using Dapper;
using Microsoft.Extensions.Logging;
using Sales.Implementation.Domain;
using System.Data;

namespace Sales.Implementation.Infrastructure;

public class CompanyRepository {

    private readonly IDbConnection _connection;
    private readonly ILogger<CompanyRepository> _logger;

    public CompanyRepository(IDbConnection connection, ILogger<CompanyRepository> logger) {
        _connection = connection;
        _logger = logger;
    }

    public async Task<CompanyContext> GetCompanyById(int id) {

        const string query = @"SELECT [Id], [Name], [Line1], [Line2], [Line3], [City], [State], [Zip]
                                FROM [Sales].[Companies]
                                WHERE [Id] = @Id;";
        var companyDto = await _connection.QuerySingleAsync<Persistance.Company>(query, new {
            Id = id
        });

        Address address = new() {
            Line1 = companyDto.Line1,
            Line2 = companyDto.Line2,
            Line3 = companyDto.Line3,
            City = companyDto.City,
            State = companyDto.State,
            Zip = companyDto.Zip,
        };

        List<CompanyRole> roles = new();
        var roleSplit = companyDto.Roles.Split(',');
        foreach (var str in roleSplit) {
            if (Enum.TryParse(str, out CompanyRole role))
                roles.Add(role);
        }

        const string contactQuery = @"SELECT [Id], [Name], [Email], [Phone]
                                    FROM [Sales].[Contacts]
                                    WHERE [Id] = @Id;";
        var contactDtos = await _connection.QueryAsync<Persistance.Contact>(contactQuery, new {
            CompanyId = id
        });

        List<Contact> contacts = new();
        foreach (var dto in contactDtos) {
            contacts.Add(new(dto.Name) {
                Id = dto.Id,
                Email = dto.Email,
                Phone = dto.Phone
            });
        }

        Company company = new(companyDto.Id, companyDto.Name, contacts, address, roles);

        _logger.LogInformation("Found company with ID {ID}, {Company}", id, company);

        return new(company);

    }

    public async Task Save(CompanyContext company) {

        _connection.Open();
        var trx = _connection.BeginTransaction();
        var events = company.Events;

        foreach (var e in events) {
            if (e is ContactAddedEvent contactAdd) {
                await ApplyContactAdd(company, contactAdd, trx);
            } else if (e is ContactRemovedEvent contactRemove) {
                await ApplyContactRemove(contactRemove, trx);
            } else if (e is AddressSetEvent addressSet) {
                await ApplyAddressSet(company, addressSet, trx);
            } else if (e is NameSetEvent nameSet) {
                await ApplyNameSet(company, nameSet, trx);
            } else if (e is RoleAddedEvent roleAdd) {
                await ApplyRoleAdd(company, roleAdd, trx);
            } else if (e is RoleRemovedEvent roleRemove) {
                await ApplyRoleRemove(company, roleRemove, trx);
            }
        }

        trx.Commit();
        _connection.Close();

        _logger.LogInformation("Applied {EventCount} events to company with ID {ID}", events.Count, company.Id);

    }

    private async Task ApplyContactAdd(CompanyContext company, ContactAddedEvent e, IDbTransaction trx) {
        const string command = @"INSERT INTO [Sales].[Contacts] ([Name], [CompanyId], [Email], [Phone])
                                VALUES (@Name, @CompanyId, @Email, @Phone);";
        await _connection.ExecuteAsync(command, new {
            e.Contact.Name,
            CompanyId = company.Id,
            e.Contact.Email,
            e.Contact.Phone,
        }, trx);
    }

    private async Task ApplyContactRemove(ContactRemovedEvent e, IDbTransaction trx) {
        const string command = @"DELETE FROM [Sales].[Contacts]
                                WHERE [Id] = @ContactId;";
        await _connection.ExecuteAsync(command, new {
            e.ContactId
        }, trx);
    }

    private async Task ApplyAddressSet(CompanyContext company, AddressSetEvent e, IDbTransaction trx) {
        const string command = @"UPDATE [Sales].[Companies]
                                SET [Line1] = @Line1, [Line2] = @Line2, [Line3] = @Line3, [City] = @City, [State] = @State, [Zip] = @Zip
                                WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            e.Address.Line1,
            e.Address.Line2,
            e.Address.Line3,
            e.Address.City,
            e.Address.State,
            e.Address.Zip,
            company.Id,
        }, trx);
    }

    private async Task ApplyNameSet(CompanyContext company, NameSetEvent e, IDbTransaction trx) {
        const string command = @"UPDATE [Sales].[Companies]
                                SET [Name] = @Name,
                                WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            company.Id,
            e.Name
        }, trx);
    }

    private async Task ApplyRoleAdd(CompanyContext company, RoleAddedEvent e, IDbTransaction trx) {

        const string query = @"SELECT [Roles] FROM [Sales].[Companies] WHERE [Id] = @Id;";

        string roles = await _connection.QuerySingleAsync<string>(query, new {
            company.Id
        }, trx);

        roles += $",{e.Role}";

        const string command = @"UPDATE [Sales].[Companies] SET [Roles] = @Roles WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Roles = roles,
            company.Id
        }, trx);

    }

    private async Task ApplyRoleRemove(CompanyContext company, RoleRemovedEvent e, IDbTransaction trx) {

        const string query = @"SELECT [Roles] FROM [Sales].[Companies] WHERE [Id] = @Id;";

        string roles = await _connection.QuerySingleAsync<string>(query, new {
            company.Id
        }, trx);

        var rolesArr = roles.Split(',')
                            .Where(r => r != e.Role.ToString());

        roles = string.Join(',', rolesArr);

        const string command = @"UPDATE [Sales].[Companies] SET [Roles] = @Roles WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(command, new {
            Roles = roles,
            company.Id
        }, trx);

    }

}
