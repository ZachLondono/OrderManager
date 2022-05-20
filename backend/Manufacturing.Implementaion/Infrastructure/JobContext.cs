using Manufacturing.Implementation.Domain;

namespace Manufacturing.Implementation.Infrastructure;

internal record JobCanceledEvent();
internal record JobScheduleEvent(DateTime ScheduleDate);
internal record JobReleasedEvent(DateTime ReleaseTimestamp);
internal record JobCompletedEvent(DateTime CompleteTimestamp);
internal record JobShippedEvent(DateTime ShipTimestamp);

public class JobContext {

    private readonly List<object> _events = new();
    private readonly Job _job;

    public IReadOnlyCollection<object> Events => _events.AsReadOnly();

    public int Id => _job.Id;

    public JobContext(Job job) {
        _job = job;
    }
    
    public void Cancel() {
        _job.Cancel();
        _events.Add(new JobCanceledEvent());
    }

    public void ReleaseToProduction() {
        _job.ReleaseToProduction();
        _events.Add(new JobReleasedEvent(DateTime.Now));
    }

    public void Complete() {
        _job.Complete();
        _events.Add(new JobCompletedEvent(DateTime.Now));
    }

    public void Ship() {
        _job.Ship();
        _events.Add(new JobShippedEvent(DateTime.Now));
    }

}