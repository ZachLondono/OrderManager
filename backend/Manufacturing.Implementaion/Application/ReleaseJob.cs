using MediatR;

namespace Manufacturing.Implementaion.Application;

internal class ReleaseJob {

    public record Command(Guid id) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {
        protected override Task Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}