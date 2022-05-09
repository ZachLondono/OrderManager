using OrderManager.Domain.Companies;
using Refit;

namespace OrderManager.ApplicationCore.Companies;

public interface ICompanyAPI {

    [Post("/Companies")]
    public Task<int> CreateCompany([Body(buffered: true)] CreateCompanyCommand command);

    public class CreateCompanyCommand {
        [AliasAs("Name")]
        public string Name { get; set; } = string.Empty;
    }

    [Put("/Companies/SetAddress")]
    public Task SetAddress([Body(buffered: true)] SetAddressCommand command);

    public class SetAddressCommand {
        [AliasAs("CompanyId")]
        public int CompanyId { get; set; }
        public string Line1 { get; set; } = string.Empty;
        public string Line2 { get; set; } = string.Empty;
        public string Line3 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zip { get; set; } = string.Empty;
    }

    [Put("/Companies/SetName")]
    public Task SetName([Body(buffered: true)] SetNameCommand command);

    public class SetNameCommand {
        public int CompanyId { get; set; }
        public string Name { get; set; } = string.Empty;
    }

    [Put("/Companies/AddRole")]
    public Task AddRole([Body(buffered: true)] RoleChangeCommand command);

    [Put("/Companies/RemoveRole")]
    public Task RemoveRole([Body(buffered:true)] RoleChangeCommand command);

    public class RoleChangeCommand {
        public int CompanyId { get; set; }
        public string Role { get; set; } = string.Empty;
    }

    [Get("/Companies/")]
    public Task<IEnumerable<CompanySummary>> GetCompanies();

    [Get("/Companies/{id}")]
    public Task<Company> GetCompany(int id);

    [Delete("/Companies/{id}")]
    public Task RemoveCompany(int id);

    [Post("/Companies/AddContact")]
    public Task<int> AddContact([Body(buffered:true)] AddContactCommand command);

    public class AddContactCommand {

        public int CompanyId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

    }

    [Put("/Companies/UpdateContact")]
    public Task UpdateContact([Body(buffered: true)] UpdateContactCommand command);

    public class UpdateContactCommand {

        public int CompanyId { get; set; }

        public int ContactId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }

    }

    [Delete("/Companies/RemoveContact")]
    public Task RemoveContact([Body(buffered:true)] RemoveContactCommand command);

    public class RemoveContactCommand {

        public int CompanyId { get; set; }

        public int ContactId { get; set; }

    }

}
