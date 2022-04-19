using Dapper;
using OrderManager.Domain.Labels;
using System.Data;
using System.Text.Json;

namespace Infrastructure.Labels.Queries;

public class GetLabelByIdQuery {

    private readonly IDbConnection _connection;

    public GetLabelByIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<LabelFieldMap?> GetLabelById(int id) {

        const string query = "SELECT ([Id], [Name], [TemplatePath], [PrintQty], [Type], [Fields]) FROM [LabelFieldMaps] WHERE [Id] = @Id;";
        var data = await _connection.QuerySingleAsync<LabelDto>(query, new {
            Id = id
        });

        LabelType type = (LabelType) Enum.Parse(typeof(LabelType), data.Type);
        var fields = JsonSerializer.Deserialize<Dictionary<string, string>>(data.Fields);
        if (fields is null) fields = new();

        return new LabelFieldMap(id,
                                data.Name ?? "",
                                data.TemplatePath ?? "",
                                data.PrintQty,
                                type, 
                                fields);

    }

    private class LabelDto {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string TemplatePath { get; set; } = "";
        public int PrintQty { get; set; }
        public string Type { get; set; } = "";
        public string Fields { get; set; } = "";
    }

}