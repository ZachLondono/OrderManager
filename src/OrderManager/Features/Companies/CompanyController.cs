using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Companies;

public class CompanyController : BaseController {

    public CompanyController(ISender sender) : base(sender) { }

    public Task<Company?> CreateCompany(string name,
                                        string contactName,
                                        string contactEmail,
                                        string contactPhone,
                                        string addressLine1,
                                        string addressLine2,
                                        string addressLine3,
                                        string city,
                                        string state,
                                        string postalCode) {
        return Sender.Send(new CreateCompany.Command(name, contactName, contactEmail, contactPhone, addressLine1, addressLine2, addressLine3, city, state, postalCode));
    }

    public Task<Company?> GetCompanyByName(string name) {
        return Sender.Send(new GetCompanyByName.Query(name));
    }

    public Task<IEnumerable<Company>> GetAllCompanies() {
        return Sender.Send(new GetAllCompanies.Query());
    }

}
