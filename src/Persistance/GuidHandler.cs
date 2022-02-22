using Dapper;
using System.Data;

namespace Persistance;

public abstract class SqliteTypeHandler<T> : SqlMapper.TypeHandler<T> {
    // Parameters are converted by Microsoft.Data.Sqlite
    // Dapper doesn't support custom handler types https://github.com/DapperLib/Dapper/issues/607
    public override void SetValue(IDbDataParameter parameter, T value)
        => parameter.Value = value;
}

public class GuidHandler : SqliteTypeHandler<Guid> {
    public override Guid Parse(object value)
        => Guid.Parse((string)value);
}