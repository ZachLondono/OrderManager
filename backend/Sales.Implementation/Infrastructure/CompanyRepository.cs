using System.Data;

namespace Sales.Implementation.Infrastructure;

public class CompanyRepository {

    private readonly IDbConnection _connection;

    public CompanyRepository(IDbConnection connection) {
        _connection = connection;
    }

    public Task<CompanyContext> GetCompanyById(int Id) => throw new NotImplementedException();

    public Task Save(CompanyContext company) => throw new NotImplementedException();

}
