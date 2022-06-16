using Manufacturing.Contracts;
using System.Data;
using Dapper;

namespace Manufacturing.Implementation.Infrastructure;

internal class WorkShopRepository {

    private readonly ManufacturingSettings _settings;

    internal WorkShopRepository(ManufacturingSettings settings) {
        _settings = settings;
    }

    internal async Task<BackLog> GetBackLog() {

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT Id, ProductClass, ProductQty
                                        FROM Manufacturing.Jobs
                                        WHERE WorkCell = NULL",

            PersistanceMode.SQLite => @"SELECT Id, ProductClass, ProductQty
                                        FROM Jobs
                                        WHERE WorkCell = NULL",

            _ => throw new InvalidDataException("Invalid DataBase mode")

        };

        var jobs =  await _settings.Connection.QueryAsync<BackLogItem>(query);

        return new() {
            Count = jobs.Count(),
            Jobs = jobs
        };

    }

    internal async Task<int?> GetJobWorkCellById(int jobId) {

        string query = _settings.PersistanceMode switch {

            PersistanceMode.SQLServer => @"SELECT WorkCell
                                            FROM Manufacturing.Jobs
                                            WHERE Id = @JobId",

            PersistanceMode.SQLite => @"SELECT WorkCell
                                        FROM Jobs
                                        WHERE Id = @JobId",

            _ => throw new InvalidDataException("Invalid DataBase mode")

        };

        return await _settings.Connection.QuerySingleOrDefaultAsync<int?>(query, new { JobId = jobId });

    }

}
