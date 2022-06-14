using OrderManager.ApplicationCore.Companies;
using OrderManager.Domain.Companies;
using Refit;

namespace LocalPersistanceAdapter;

public class LocalCompanyController : ICompanyController {

    public Task<int> AddContact([Body(true)] ICompanyController.AddContactCommand command) {
        throw new NotImplementedException();
    }

    public Task AddRole([Body(true)] ICompanyController.RoleChangeCommand command) {
        throw new NotImplementedException();
    }

    public Task<int> CreateCompany([Body(true)] ICompanyController.CreateCompanyCommand command) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CompanySummary>> GetCompanies() {
        throw new NotImplementedException();
    }

    public Task<Company> GetCompany(int id) {
        throw new NotImplementedException();
    }

    public Task RemoveCompany(int id) {
        throw new NotImplementedException();
    }

    public Task RemoveContact([Body(true)] ICompanyController.RemoveContactCommand command) {
        throw new NotImplementedException();
    }

    public Task RemoveRole([Body(true)] ICompanyController.RoleChangeCommand command) {
        throw new NotImplementedException();
    }

    public Task SetAddress([Body(true)] ICompanyController.SetAddressCommand command) {
        throw new NotImplementedException();
    }

    public Task SetName([Body(true)] ICompanyController.SetNameCommand command) {
        throw new NotImplementedException();
    }

    public Task UpdateContact([Body(true)] ICompanyController.UpdateContactCommand command) {
        throw new NotImplementedException();
    }

}