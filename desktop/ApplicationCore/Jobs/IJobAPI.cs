using OrderManager.Domain.Jobs;
using Refit;

namespace OrderManager.ApplicationCore.Jobs;

public interface IJobAPI {

    [Post("Manufacturing/CompleteJob/{id}")]
    public Task CompleteJob(int id);

    [Post("Manufacturing/ReleaseJob/{id}")]
    public Task ReleaseJob(int id);

    [Post("Manufacturing/ShipJob/{id}")]
    public Task ShipJob(int id);

    [Get("Manufacturing/{id}")]
    public Task<Job> GetJob(int id);

    [Get("Manufacturing/GetJobs")]
    public Task<IEnumerable<JobSummary>> GetJobs();

}
