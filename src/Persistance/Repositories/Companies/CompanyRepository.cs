namespace Persistance.Repositories.Companies;

public class CompanyRepository : BaseRepository, ICompanyRepository {
    public CompanyRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) { }

    public CompanyDAO CreateCompany(string name) {
        const string sql = @"INSERT INTO [Companies] ([Name])
                        VALUES @Name
                        RETURN Id;";
        int id = QuerySingleOrDefault<int>(sql, new { Name = name });
        return new() {
            Id = id,
            Name = name
        };
    }

    public CompanyDAO GetCompanyById(int id) {
        const string query = @"SELECT [Id], [Name], [Contact], [Address1], [Address2], [Address3], [City], [State], [Zip] FROM [Companies] WHERE [Id] = @Id;";
        return QuerySingleOrDefault<CompanyDAO>(query, new { Id = id });
    }

    public void UpdateCompany(CompanyDAO company) {
        const string query = @"UPDATE [Companies]
                        SET [Id] = [@Id], [Name] = [@Name], [Contact] = [@Contact], [Address1] = [@Address1], [Address2] = [@Address2], [Address3] = [@Address3], [City] = [@City], [State] = [@State], [Zip] = [@Zip]
                        WHERE [Id] = [@Id];";
        Execute(query, company);
    }
}
