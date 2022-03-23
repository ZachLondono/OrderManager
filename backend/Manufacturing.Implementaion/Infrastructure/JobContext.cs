using Manufacturing.Implementaion.Domain;

namespace Manufacturing.Implementaion.Infrastructure;

internal record JobStatusChangeEvent(ManufacturingStatus Status);

internal class JobContext {

    private List<object> _events = new();
    private Job _job;
    
    public IReadOnlyCollection<object> Events => _events.AsReadOnly();

    public JobContext(Job job) {
        _job = job;
    }

    public void ReleaseToProduction() => throw new NotImplementedException();

    public void Complete() => throw new NotImplementedException();

    public void Ship() => throw new NotImplementedException();

}