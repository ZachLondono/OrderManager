using System.Data;

namespace Manufacturing.Contracts;

public class ManufacturingSettings {

    public PersistanceMode PersistanceMode { get; init; }

    public IDbConnection? Connection { get; init; }

}

public enum PersistanceMode {
    SQLite,
    SQLServer
}