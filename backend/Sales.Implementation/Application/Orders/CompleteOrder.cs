using Manufacturing.Contracts;
using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Orders;

public static class CompleteOrder {

    public class Handler : INotificationHandler<JobCompleteNotification> {

        private readonly OrderRepository _orderRepo;

        public Handler(OrderRepository orderRepo) {
            _orderRepo = orderRepo;
        }

        public async Task Handle(JobCompleteNotification notification, CancellationToken cancellationToken) {
            var order = await _orderRepo.GetOrderById(notification.Job.OrderId);
            order.CompleteOrder();
            await _orderRepo.Save(order);
        }

    }

}
