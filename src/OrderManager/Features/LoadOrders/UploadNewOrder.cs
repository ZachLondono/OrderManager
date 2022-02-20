using MediatR;
using OrderManager.Shared.Notifications;
using System.Threading;
using System.Threading.Tasks;

namespace OrderManager.Features.LoadOrders;

internal class UploadNewOrder {

    public record Command() : IRequest;

    public class Handler : IRequestHandler<Command> {

        private readonly IPublisher _publisher;

        public Handler(IPublisher publisher) {
            _publisher = publisher;
        }

        public Task<Unit> Handle(Command request, CancellationToken cancellationToken) {

            // TODO: Upload order to Database

            int newId = 0;

            _publisher.Publish(new OrderUploadedNotification(newId));

            return Unit.Task;

        }

    }

}
