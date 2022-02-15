namespace Persistance.Repositories.Parts.PartAttributes;

public class PartAttributeRepository : BaseRepository, IPartAttributeRepository {

    public PartAttributeRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) { }

    public PartAttributeDAO CreateAttribute(int partId, string name) {
        string query = "INSERT INTO [PartAttributes] ([PartId] [Name]) VALUES ([@PartId], [@Name]) RETURNING Id;";
        int newId = QuerySingleOrDefault<int>(query, new { PartId = partId, Name = name });

        return new() {
            Id = newId,
            PartId = partId,
            Name = name
        };
    }

    public IEnumerable<PartAttributeDAO> GetAttributesByPartId(int partId) {
        string query = "SELECT [Id], [PartId], [Name] FROM [PartAttributes] WHERE [PartId] = [@PartId];";
        return Query<PartAttributeDAO>(query, new { PartId = partId });
    }

    public void UpdateAttribute(PartAttributeDAO attribute) {
        string sql = "UPDATE [PartAttributes] SET [PartId] = [@PartId], [Name] = [@Name] WHERE [PartId] = [@PartId];";
        Execute(sql, attribute);
    }
}