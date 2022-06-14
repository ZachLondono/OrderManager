using System.Data;

namespace Catalog.Contracts;

public class CatalogSettings {

    public PersistanceMode PersistanceMode { get; init; }

    public IDbConnection? Connection { get; init; }

}

public enum PersistanceMode {
    SQLite,
    SQLServer
}