using Manufacturing.Implementation.Infrastructure;
using MediatR;
using Sales.Contracts;

namespace Manufacturing.Implementation.Application;

public static class CancleJob { 

    public class Handler : INotificationHandler<OrderVoidNotification> {

        private readonly JobRepository _repo;

        public Handler(JobRepository repo) {
            _repo = repo;
        }

        public async Task Handle(OrderVoidNotification notification, CancellationToken cancellationToken) {
            var job = await _repo.GetJobById(notification.OrderId);
            job.Cancel();
            await _repo.Save(job);
        }

    }
}
