using OrderManager.ApplicationCore.Jobs;
using OrderManager.Domain.Jobs;

namespace LocalPersistanceAdapter;

public class LocalJobController : IJobController {
    
    public Task CompleteJob(int id) {
        throw new NotImplementedException();
    }

    public Task<Job> GetJob(int id) {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<JobSummary>> GetJobs() {
        throw new NotImplementedException();
    }

    public Task ReleaseJob(int id) {
        throw new NotImplementedException();
    }

    public Task ShipJob(int id) {
        throw new NotImplementedException();
    }

}