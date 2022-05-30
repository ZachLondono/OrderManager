using Manufacturing.Implementation.Domain;

namespace Manufacturing.Implementation.Infrastructure;

public class WorkCellContext {

    private readonly List<object> _events = new();
    private readonly WorkCell _workCell;

    public WorkCellContext(WorkCell workCell) {
        _workCell = workCell;
    }

    public List<object> GetEvents() => _events;

    public void SetAlias(string alias) {
        _workCell.SetAlias(alias);
        _events.Add(new AliasSet(alias));
    }

    public void SetExpectedMaxOutput(int expectedMaxOutput) {
        _workCell.SetExpectedMaxOutput(expectedMaxOutput);
        _events.Add(new OutputSet(expectedMaxOutput));
    }

    /// <summary>
    /// If the job does not already exist in the WorkCell, it will be added. If the job already exists in the WorkCell, it will be rescheduled to the new date.
    /// </summary>
    /// <param name="jobId">ID of the Job to schedule</param>
    /// <param name="scheduledDate">Manufacturing date to schedule the Job for</param>
    public void ScheduleJob(int jobId, DateTime scheduledDate) {

        ScheduledJob? job = _workCell.Jobs.SingleOrDefault(j => j.JobId == jobId);
        if (job is null) {
            
            _workCell.Jobs.Add(new() {
                JobId = jobId,
                ScheduledDate = scheduledDate
            });

            _events.Add(new JobAdded(jobId, scheduledDate));

        } else {

            job.ScheduledDate = scheduledDate;

            _events.Add(new JobRescheduled(jobId, scheduledDate));

        }

    }

    public void RemoveJob(int jobId) {
        ScheduledJob? job = _workCell.Jobs.SingleOrDefault(j => j.JobId == jobId);
        if (job is null) return;
        _workCell.Jobs.Remove(job);
        _events.Add(new JobRemoved(jobId));
    }

    public record AliasSet(string Alias);
    public record OutputSet(int Output);
    public record JobAdded(int JobId, DateTime ScheduledDate);
    public record JobRescheduled(int JobId, DateTime ScheduledDate);
    public record JobRemoved(int JobId);

}
