using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

internal class RemoveContact {

    public record Command(Guid CompanyId, string Name) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var company = await _repo.GetCompanyById(request.CompanyId);
            company.RemoveContact(request.Name);
            await _repo.Save(company);
        }
    }

}
