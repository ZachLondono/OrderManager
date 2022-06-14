using OrderManager.Domain.Jobs;
using Refit;

namespace OrderManager.ApplicationCore.Jobs;

public interface IJobController {

    [Post("/CompleteJob/{id}")]
    public Task CompleteJob(int id);

    [Post("/ReleaseJob/{id}")]
    public Task ReleaseJob(int id);

    [Post("/ShipJob/{id}")]
    public Task ShipJob(int id);

    [Get("/{id}")]
    public Task<Job> GetJob(int id);

    [Get("/GetJobs")]
    public Task<IEnumerable<JobSummary>> GetJobs();

}
