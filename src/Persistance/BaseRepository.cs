using Microsoft.Data.Sqlite;
using Dapper;

namespace Persistance;

public abstract class BaseRepository {

    protected readonly string _connectionString;

    public BaseRepository(ConnectionStringManager connectionStringManager) {
        _connectionString = connectionStringManager.GetConnectionString;
    }

    public void Execute(string sql, object? param = null) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        connection.Execute(sql, param);
        connection.Close();
    }

    public IEnumerable<T> Query<T>(string query, object? param = null) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        IEnumerable<T> result = connection.Query<T>(query, param);
        connection.Close();
        return result;
    }

    public T QuerySingleOrDefault<T>(string query, object? param = null) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        T result = connection.QuerySingleOrDefault<T>(query, param);
        connection.Close();
        return result;
    }

    public T Query<T>(Func<SqliteConnection, T> query) {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();
        T result = query.Invoke(connection);
        connection.Close();
        return result;
    }

}
