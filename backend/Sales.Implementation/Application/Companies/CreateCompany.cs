using MediatR;

namespace Sales.Implementation.Application.Companies;

internal class CreateCompany {

    public record Command(string Name) : IRequest<Guid>;

    public class Handler : IRequestHandler<Command, Guid> {
        public Task<Guid> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}