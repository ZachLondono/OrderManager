using MediatR;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Orders;

internal class ConfirmOrder {

    public record Command(Guid OrderId) : IRequest;

    public class Handler : AsyncRequestHandler<Command> {

        private readonly OrderRepository _orderRepo;

        public Handler(OrderRepository orderRepo) {
            _orderRepo = orderRepo;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            var order = await _orderRepo.GetOrderById(request.OrderId);
            order.ConfirmOrder();
            await _orderRepo.Save(order);
        }
    }

}