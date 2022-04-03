using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

public class AddContact {

    public record Command(int CompanyId, string Name, string? Email, string? Phone) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var company = await _repo.GetCompanyById(request.CompanyId);
            company.AddContact(new(request.Name) {
                Email = request.Email,
                Phone = request.Phone
            });
            await _repo.Save(company);
        }
    }

}
