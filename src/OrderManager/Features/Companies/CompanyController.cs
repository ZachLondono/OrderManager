using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Companies;

public class CompanyController {

    private readonly ISender _sender;

    public CompanyController(ISender sender) {
        _sender = sender;
    }

    public Task<Company> CreateCompany(string name,
                                        string contactName,
                                        string contactEmail,
                                        string contactPhone,
                                        string addressLine1,
                                        string addressLine2,
                                        string addressLine3,
                                        string city,
                                        string state,
                                        string postalCode) {
        return _sender.Send(new CreateCompany.Command(name, contactName, contactEmail, contactPhone, addressLine1, addressLine2, addressLine3, city, state, postalCode));
    }


}
