namespace Persistance.Repositories.Companies;

public interface ICompanyRepository {

    public CompanyDAO CreateCompany(string Name);

    public CompanyDAO GetCompanyById(int id);

    public void UpdateCompany(CompanyDAO company);

}