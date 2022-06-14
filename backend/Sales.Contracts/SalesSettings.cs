using System.Data;

namespace Sales.Contracts;

public class SalesSettings {

    public PersistanceMode PersistanceMode { get; init; }

    public IDbConnection? Connection { get; init; }

}

public enum PersistanceMode {
    SQLite,
    SQLServer
}
