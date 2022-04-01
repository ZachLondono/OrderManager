using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Companies;

internal class CreateCompany {

    public record Command(string Name) : IRequest<int>;

    public class Handler : IRequestHandler<Command, int> {

        private readonly CompanyRepository _repo;

        public Handler(CompanyRepository repo) {
            _repo = repo;
        }

        public Task<int> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}