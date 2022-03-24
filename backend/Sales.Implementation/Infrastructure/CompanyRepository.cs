using System.Data;

namespace Sales.Implementation.Infrastructure;

internal class CompanyRepository {

    private readonly IDbConnection _connection;

    public CompanyRepository(IDbConnection connection) {
        _connection = connection;
    }

    public CompanyContext GetCompanyById(Guid Id) => throw new NotImplementedException();

    public void Save(CompanyContext company) => throw new NotImplementedException();

}
