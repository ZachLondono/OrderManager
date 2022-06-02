using Manufacturing.Contracts;
using System.Data;
using Dapper;

namespace Manufacturing.Implementation.Infrastructure;

internal class WorkShopRepository {

    private readonly IDbConnection _connection;

    internal WorkShopRepository(IDbConnection connection) {
        _connection = connection;
    }

    internal async Task<BackLog> GetBackLog() {

        const string query = @"SELECT Id, ProductClass, ProductQty
                                FROM Manufacturing.Jobs
                                WHERE WorkCell = NULL";

        var jobs =  await _connection.QueryAsync<BackLogItem>(query);

        return new() {
            Count = jobs.Count(),
            Jobs = jobs
        };

    }

    internal async Task<int?> GetJobWorkCellById(int jobId) {

        const string query = @"SELECT WorkCell
                                FROM Manufacturing.Jobs
                                WHERE Id = @JobId";

        return await _connection.QuerySingleOrDefaultAsync<int?>(query, new { JobId = jobId });

    }

}
