using Manufacturing.Implementation.Domain;

namespace Manufacturing.Implementation.Infrastructure;

internal record JobStatusChangeEvent(ManufacturingStatus Status);

internal class JobContext {

    private List<object> _events = new();
    private Job _job;
    
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

}