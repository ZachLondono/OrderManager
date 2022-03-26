using MediatR;
using Sales.Implementation.Domain;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

internal class RemoveRole {

    public record Command(Guid CompanyId, string Role) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var company = await _repo.GetCompanyById(request.CompanyId);
            CompanyRole role = (CompanyRole)Enum.Parse(typeof(CompanyRole), request.Role);
            company.RemoveRole(role);
            await _repo.Save(company);
        }
    }

}