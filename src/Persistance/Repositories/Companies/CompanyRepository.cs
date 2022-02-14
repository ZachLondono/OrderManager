namespace Persistance.Repositories.Companies;

public class CompanyRepository : BaseRepository, ICompanyRepository {
    public CompanyRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) { }

    public CompanyDAO CreateCompany(string name) {
        string sql = @"INSERT INTO [Companies] ([Name])
                        VALUES @Name
                        RETURN Id;";
        int id = QuerySingleOrDefault<int>(sql, new { Name = name });
        return new() {
            Id = id,
            Name = name
        };
    }

    public CompanyDAO GetCompanyById(int id) {
        string query = @"SELECT [Id], [Name] FROM [Companies] WHERE [Id] = @Id;";
        return QuerySingleOrDefault<CompanyDAO>(query, new { Id = id });
    }

    public void UpdateCompany(CompanyDAO company) {
        string query = @"UPDATE [Companies]
                        SET [Id] = @Id, [Name] = @Name
                        WHERE [Id] = [@Id];";
        Execute(query, company);
    }
}
