namespace Persistance.Repositories.Parts;

public class PartRepository : BaseRepository, IPartRepository {
    public PartRepository(ConnectionStringManager connectionStringManager) : base(connectionStringManager) {
    }

    public PartDAO CreatePart(int productId, string name) {
        string sql = @"INSERT INTO [Parts] ([ProductId], [Name])
                        VALUES ([@ProductId], [@Name])
                        RETURNING Id;";
        int newId = QuerySingleOrDefault<int>(sql, new { ProductId = productId, Name = name});
        return new() {
            Id = newId,
            ProductId = productId,
            Name = name
        };

    }

    public IEnumerable<PartDAO> GetPartsByProduct(int productId) {
        string query = @"SELECT [Id], [ProductId], [Name] FROM [Parts]
                        WHERE [ProductId] = [@ProductId];";
        return Query<PartDAO>(query, new { ProductId = productId });
    }

    public void UpdatePart(PartDAO part) {
        string sql = @"UPDATE [Parts]
                        SET [ProductId] = [@ProductId], [Name] = [@Name]
                        WHERE [Id] = [@Id];";
        Execute(sql, part);
    }
}