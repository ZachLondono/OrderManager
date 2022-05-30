using FluentValidation;
using MediatR;
using Sales.Contracts;
using Sales.Implementation.Infrastructure;

namespace Sales.Implementation.Application.Orders;

public class VoidOrder {

    public record Command(int OrderId) : IRequest;

    public class Validation : AbstractValidator<Command> {

        public Validation() {

            RuleFor(x => x.OrderId)
                .NotEqual(0)
                .WithMessage("Invalid order id");

        }

    }

    public class Handler : AsyncRequestHandler<Command> {

        private readonly OrderRepository _orderRepo;
        private readonly IPublisher _publisher;

        public Handler(OrderRepository orderRepo, IPublisher publisher) {
            _orderRepo = orderRepo;
            _publisher = publisher;
        }

        protected override async Task Handle(Command request, CancellationToken cancellationToken) {
            
            var order = await _orderRepo.GetOrderById(request.OrderId);
            order.VoidOrder();
            await _orderRepo.Save(order);

            await _publisher.Publish(new OrderVoidNotification(request.OrderId));

        }
    }

}