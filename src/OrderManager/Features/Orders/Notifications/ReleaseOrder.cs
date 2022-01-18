using MediatR;
using OrderManager.ApplicationCore.Domain;

namespace OrderManager.ApplicationCore.Features.Orders;

public class ReleaseOrder {

    public record Notification(Order Order) : INotification;

    internal class DBCutListHandler : INotificationHandler<Notification> {
        public Task Handle(Notification notification, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

    internal class InvoiceHandler : INotificationHandler<Notification> {
        public Task Handle(Notification notification, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

    internal class GoogleSheetUpdateHandler : INotificationHandler<Notification> {
        public Task Handle(Notification notification, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

    internal class MaterialTrackingHandler : INotificationHandler<Notification> {
        public Task Handle(Notification notification, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }

}
