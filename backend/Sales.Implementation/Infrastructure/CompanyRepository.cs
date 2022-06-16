using Dapper;
using Microsoft.Extensions.Logging;
using Sales.Contracts;
using Sales.Implementation.Domain;
using System.Data;

namespace Sales.Implementation.Infrastructure;

public class CompanyRepository {

    private readonly SalesSettings _settings;
    private readonly ILogger<CompanyRepository> _logger;

    public CompanyRepository(SalesSettings settings, ILogger<CompanyRepository> logger) {
        _settings = settings;
        _logger = logger;
    }

    public async Task<CompanyContext> GetCompanyById(int id) {

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT [Id], [Name], [Email], [Line1], [Line2], [Line3], [City], [State], [Zip]
                                            FROM [Sales].[Companies]
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"SELECT [Id], [Name], [Email], [Line1], [Line2], [Line3], [City], [State], [Zip]
                                        FROM [Companies]
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        var companyDto = await _settings.Connection.QuerySingleAsync<Persistance.Company>(query, new {
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

        string contactQuery = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT [Id], [Name], [Email], [Phone]
                                        FROM [Sales].[Contacts]
                                        WHERE [CompanyId] = @Id;",

            PersistanceMode.SQLite => @"SELECT [Id], [Name], [Email], [Phone]
                                        FROM [Contacts]
                                        WHERE [CompanyId] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        var contactDtos = await _settings.Connection.QueryAsync<Persistance.Contact>(contactQuery, new {
            Id = id
        });

        List<Contact> contacts = new();
        foreach (var dto in contactDtos) {
            contacts.Add(new(dto.Name) {
                Id = dto.Id,
                Email = dto.Email,
                Phone = dto.Phone
            });
        }

        Company company = new(companyDto.Id, companyDto.Name, companyDto.Email, contacts, address, roles);

        _logger.LogInformation("Found company with ID {ID}, {Company}", id, company);

        return new(company);

    }

    public async Task Save(CompanyContext company) {

        if (_settings.Connection is null) return;

        _settings.Connection.Open();
        var trx = _settings.Connection.BeginTransaction();
        var events = company.Events;

        foreach (var e in events) {
            if (e is ContactAddedEvent contactAdd) {
                await ApplyContactAdd(company, contactAdd, trx);
            } else if (e is ContactRemovedEvent contactRemove) {
                await ApplyContactRemove(contactRemove, trx);
            } else if (e is ContactUpdatedEvent contactUpdated) {
                await ApplyContactUpdated(contactUpdated, trx);
            } else if (e is AddressSetEvent addressSet) {
                await ApplyAddressSet(company, addressSet, trx);
            } else if (e is NameSetEvent nameSet) {
                await ApplyNameSet(company, nameSet, trx);
            } else if (e is EmailSetEvent emailSet) {
                await ApplyEmailSet(company, emailSet, trx);
            } else if (e is RoleAddedEvent roleAdd) {
                await ApplyRoleAdd(company, roleAdd, trx);
            } else if (e is RoleRemovedEvent roleRemove) {
                await ApplyRoleRemove(company, roleRemove, trx);
            }
        }

        trx.Commit();
        _settings.Connection.Close();

        _logger.LogInformation("Applied {EventCount} events to company with ID {ID}", events.Count, company.Id);

    }

    private async Task ApplyContactAdd(CompanyContext company, ContactAddedEvent e, IDbTransaction trx) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"INSERT INTO [Sales].[Contacts] ([Name], [CompanyId], [Email], [Phone])
                                        VALUES (@Name, @CompanyId, @Email, @Phone);",

            PersistanceMode.SQLite => @"INSERT INTO [Contacts] ([Name], [CompanyId], [Email], [Phone])
                                        VALUES (@Name, @CompanyId, @Email, @Phone);",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
            e.Contact.Name,
            CompanyId = company.Id,
            e.Contact.Email,
            e.Contact.Phone,
        }, trx);
    }

    private async Task ApplyContactRemove(ContactRemovedEvent e, IDbTransaction trx) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"DELETE FROM [Sales].[Contacts]
                                            WHERE [Id] = @ContactId;",

            PersistanceMode.SQLite => @"DELETE FROM [Contacts]
                                        WHERE [Id] = @ContactId;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
            e.ContactId
        }, trx);
    }

    private async Task ApplyContactUpdated(ContactUpdatedEvent e, IDbTransaction trx) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"UPDATE [Sales].[Contacts]
                                            SET [Name] = @Name, [Email] = @Email, [Phone] = @Phone
                                            WHERE [Id] = @ContactId;",

            PersistanceMode.SQLite => @"UPDATE [Contacts]
                                        SET [Name] = @Name, [Email] = @Email, [Phone] = @Phone
                                        WHERE [Id] = @ContactId;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
            ContactId = e.Contact.Id,
            e.Contact.Name,
            e.Contact.Email,
            e.Contact.Phone,
        }, trx);
    }

    private async Task ApplyAddressSet(CompanyContext company, AddressSetEvent e, IDbTransaction trx) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"UPDATE [Sales].[Companies]
                                            SET [Line1] = @Line1, [Line2] = @Line2, [Line3] = @Line3, [City] = @City, [State] = @State, [Zip] = @Zip
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"UPDATE [Companies]
                                        SET [Line1] = @Line1, [Line2] = @Line2, [Line3] = @Line3, [City] = @City, [State] = @State, [Zip] = @Zip
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
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
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"UPDATE [Sales].[Companies]
                                            SET [Name] = @Name
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"UPDATE [Companies]
                                        SET [Name] = @Name
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
            company.Id,
            e.Name
        }, trx);
    }

    private async Task ApplyEmailSet(CompanyContext company, EmailSetEvent e, IDbTransaction trx) {
        
        string command = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"UPDATE [Sales].[Companies]
                                            SET [Email] = @Email
                                            WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"UPDATE [Companies]
                                        SET [Email] = @Email
                                        WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        await _settings.Connection.ExecuteAsync(command, new {
            company.Id,
            e.Email
        }, trx);
    }

    private async Task ApplyRoleAdd(CompanyContext company, RoleAddedEvent e, IDbTransaction trx) {

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT [Roles] FROM [Sales].[Companies] WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"SELECT [Roles] FROM [Companies] WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        string roles = await _settings.Connection.QuerySingleAsync<string>(query, new {
            company.Id
        }, trx);

        roles += $",{e.Role}";

        const string command = @"UPDATE [Sales].[Companies] SET [Roles] = @Roles WHERE [Id] = @Id;";
        await _settings.Connection.ExecuteAsync(command, new {
            Roles = roles,
            company.Id
        }, trx);

    }

    private async Task ApplyRoleRemove(CompanyContext company, RoleRemovedEvent e, IDbTransaction trx) {

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT [Roles] FROM [Sales].[Companies] WHERE [Id] = @Id;",

            PersistanceMode.SQLite => @"SELECT [Roles] FROM [Companies] WHERE [Id] = @Id;",

            _ => throw new InvalidDataException("Invalid persistance mode")

        };

        string roles = await _settings.Connection.QuerySingleAsync<string>(query, new {
            company.Id
        }, trx);

        //TODO remove empty strings from list
        var rolesArr = roles.Split(',')
                            .Where(r => r != e.Role.ToString() && !string.IsNullOrEmpty(r) && !string.IsNullOrWhiteSpace(r));

        roles = string.Join(',', rolesArr);

        const string command = @"UPDATE [Sales].[Companies] SET [Roles] = @Roles WHERE [Id] = @Id;";
        await _settings.Connection.ExecuteAsync(command, new {
            Roles = roles,
            company.Id
        }, trx);

    }

}
