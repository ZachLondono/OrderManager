using System.Data;

namespace Manufacturing.Implementation.Infrastructure;

public class JobRepository {

    private IDbConnection _connection;

    public JobRepository(IDbConnection connection) {
        _connection = connection;
    }

    public Task<JobContext> GetJobById(Guid jobId) => throw new NotImplementedException();

    public Task Save(JobContext context) => throw new NotImplementedException();

}
