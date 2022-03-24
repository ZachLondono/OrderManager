using MediatR;

namespace Sales.Implementation.Application.Companies;

internal class SetName {

    public record Command(Guid CompanyId, string Name) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
