using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Orders;

public class CompleteOrder {

    public record Notification(Order order) : INotification;

    internal class ShippingHandler : INotificationHandler<Notification> {
        public Task Handle(Notification notification, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
