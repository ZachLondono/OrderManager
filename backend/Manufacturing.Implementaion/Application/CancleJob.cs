using MediatR;
using Sales.Contracts;

namespace Manufacturing.Implementaion.Application;

internal class CancleJob : INotificationHandler<OrderVoidNotification> {

    public Task Handle(OrderVoidNotification notification, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }

}
