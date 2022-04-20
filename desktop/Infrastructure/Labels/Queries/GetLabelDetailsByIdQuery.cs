using Dapper;
using OrderManager.Domain.Labels;
using System.Data;
using System.Text.Json;

namespace Infrastructure.Labels.Queries;

public class GetLabelDetailsByIdQuery {

    private readonly IDbConnection _connection;

    public GetLabelDetailsByIdQuery(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<LabelFieldMapDetails> GetLabelDetailsById(int id) {

        const string query = @"SELECT [Id], [Name], [TemplatePath], [PrintQty], [Type], [Fields]
                                FROM [LabelFieldMaps]
                                WHERE Id = @Id;";

        var result = await _connection.QuerySingleAsync(query, new {
            Id = id
        });

        return new LabelFieldMapDetails {
            Id = result.Id,
            Name = result.Name ?? string.Empty,
            TemplatePath = result.TemplatePath ?? string.Empty,
            PrintQty = result.PrintQty ?? 0,
            Type = (LabelType) Enum.Parse(typeof(LabelType), (string) result.Type),
            Fields = JsonSerializer.Deserialize<Dictionary<string,string>>(result.Fields) ?? new Dictionary<string,string>()
        };

    }

}
