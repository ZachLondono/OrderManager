using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Triggers;

public class CreateTrigger {

    public record Command(string Name, TriggerType Type, ITriggerAction[] Actions) : IRequest<Trigger>;

    internal class Handler : IRequestHandler<Command, Trigger> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public Task<Trigger> Handle(Command request, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

    }

}