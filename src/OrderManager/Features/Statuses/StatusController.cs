using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Statuses;

public class StatusController : BaseController {

    public StatusController(ISender sender) : base(sender) { }

    public Task<Status> CreateStatus(string name, int level) {
        return Sender.Send(new CreateStatus.Command(name, level));
    }

    public Task<IEnumerable<Status>> GetAllStatuses() {
        return Sender.Send(new GetAllStatuses.Query());
    }

}