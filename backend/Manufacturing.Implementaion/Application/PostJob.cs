using MediatR;
using Sales.Contracts;

namespace Manufacturing.Implementation.Application;

internal class PostJob : INotificationHandler<OrderConfirmedNotification> {

    public Task Handle(OrderConfirmedNotification notification, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

}