using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Priorities;

public class PriorityController : BaseController {

    public PriorityController(ISender sender) : base(sender) { }

    public Task<Priority> CreatePriority(string name, int level) {
        return Sender.Send(new CreatePriority.Command(name, level));
    }

    public Task<IEnumerable<Priority>> GetAllPriorities() {
        return Sender.Send(new GetAllPriorities.Query());
    }

    public Task UpdatePriority(Priority priority) {
        return Sender.Send(new UpdatePriority.Command(priority));
    }

}