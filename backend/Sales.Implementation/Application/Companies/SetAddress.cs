using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

public class SetAddress {

    public record Command(int CompanyId, string Line1, string Line2, string Line3, string City, string State, string Zip) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var company = await _repo.GetCompanyById(request.CompanyId);
            company.SetAddress(request.Line1, request.Line2, request.Line3, request.City, request.State, request.Zip);
            await _repo.Save(company);
        }
    }

}
