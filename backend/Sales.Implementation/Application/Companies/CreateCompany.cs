using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

internal class CreateCompany {

    public record Command(string Name) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        public Task<Guid> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}