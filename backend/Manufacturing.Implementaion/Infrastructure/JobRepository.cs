using System.Data;

namespace Manufacturing.Implementation.Infrastructure;

internal class JobRepository {

    private IDbConnection _connection;

    public JobRepository(IDbConnection connection) {
        _connection = connection;
    }

    public JobContext GetJobById(Guid jobId) => throw new NotImplementedException();

    public void Save(JobContext context) => throw new NotImplementedException();

}
