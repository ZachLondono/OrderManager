using Manufacturing.Contracts;
using Manufacturing.Implementation.Domain;

namespace Manufacturing.Implementation.Infrastructure;

internal record JobStatusChangeEvent(ManufacturingStatus Status);

public class JobContext {

    private readonly List<object> _events = new();
    private readonly Job _job;

    public IReadOnlyCollection<object> Events => _events.AsReadOnly();

    public JobContext(Job job) {
        _job = job;
    }
    
    public void Cancel() {
        _job.Cancel();
        _events.Add(new JobStatusChangeEvent(ManufacturingStatus.Canceled));
    }

    public void ReleaseToProduction() {
        _job.ReleaseToProduction();
        _events.Add(new JobStatusChangeEvent(ManufacturingStatus.InProgress));
    }

    public void Complete() {
        _job.Complete();
        _events.Add(new JobStatusChangeEvent(ManufacturingStatus.Complete));
    }

    public void Ship() {
        _job.Ship();
        _events.Add(new JobStatusChangeEvent(ManufacturingStatus.Shipped));
    }

    public JobDetails Details() => new() {
        Name = _job.Name,
        Number = _job.Number,
        Customer = _job.Customer,
        ItemCount = _job.ItemCount,
        Vendor = _job.Vendor,
        ReleaseDate = _job.ReleaseDate,
        CompleteDate = _job.CompleteDate,
        ShippedDate = _job.ShippedDate,
    };

}