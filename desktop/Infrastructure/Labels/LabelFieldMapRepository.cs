using Dapper;
using Infrastructure.Labels.Queries;
using OrderManager.ApplicationCore.Labels;
using OrderManager.Domain.Labels;
using System.Data;
using System.Text.Json;

namespace Infrastructure.Labels;

public class LabelFieldMapRepository : ILabelFieldMapRepository {

    private readonly IDbConnection _connection;

    public LabelFieldMapRepository(IDbConnection connection) {
        _connection = connection;
    }

    public async Task<LabelFieldMapContext> Add(string name, string templatePath, LabelType labelType, Dictionary<string, string> fields) {
        const string query = @"INSERT INTO [LabelFieldMaps] ([Name], [TemplatePath], [PrintQty], [Type], [Fields])
                                VALUES (@Name, @TemplatePath, @PrintQty, @LabelType, @Fields)
                                returning [Id];";

        var json = JsonSerializer.Serialize(fields);
        int printQty = 1;
        var newId = await _connection.QuerySingleAsync<int>(query, new {
            Name = name,
            TemplatePath = templatePath,
            PrintQty = printQty,
            LabelType = labelType.ToString(),
            Fields = json
        });

        return new(new(newId, name, templatePath, printQty, labelType, fields));
    }

    public async Task<LabelFieldMapContext> GetById(int id) {
        var query = new GetLabelByIdQuery(_connection);
        var labelmap = await query.GetLabelById(id);
        if (labelmap is null) throw new InvalidDataException($"Could not find label field map with id '{id}'");
        return new(labelmap);
    }

    public async Task Remove(int id) {
        const string sql = "DELETE FROM [LabelFieldMaps] WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task Save(LabelFieldMapContext context) {
        
        bool openCloseConnection = true;
        if (_connection.State == ConnectionState.Open) openCloseConnection = false;

        if (openCloseConnection) _connection.Open();
        var trx = _connection.BeginTransaction();

        var events = context.Events;
        int id = context.Id;

        foreach (var ev in events) {

            if (ev is LabelNameChangeEvent nameChangeEvent) {
                await ApplyNameChangeEvent(trx, id, nameChangeEvent);
            } else if (ev is LabelTypeChangeEvent typeChangeEvent) {
                await ApplyTypeChangeEvent(trx, id, typeChangeEvent);
            } else if (ev is LabelPrintQtyChangeEvent printQtyChangeEvent) {
                await ApplyPrintQtyChangeEvent(trx, id, printQtyChangeEvent);
            } else if (ev is LabelFieldFormulaSetEvent fieldFormulaSetEvent) {
                await ApplyFieldFormulaSetEvent(trx, id, fieldFormulaSetEvent);
            }

        }

        context.ClearEvents();

        trx.Commit();
        if (openCloseConnection) _connection.Close();

    }

    private async Task ApplyNameChangeEvent(IDbTransaction trx, int labelId, LabelNameChangeEvent ev) {
        const string sql = "UPDATE [LabelFieldMaps] SET [Name] = @Name WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = labelId,
            Name = ev.Name
        }, trx);
    }

    private async Task ApplyTypeChangeEvent(IDbTransaction trx, int labelId, LabelTypeChangeEvent ev) {
        const string sql = "UPDATE [LabelFieldMaps] SET [Type] = @Type WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = labelId,
            Type = ev.Type.ToString()
        }, trx);
    }

    private async Task ApplyPrintQtyChangeEvent(IDbTransaction trx, int labelId, LabelPrintQtyChangeEvent ev) {
        const string sql = "UPDATE [LabelFieldMaps] SET [PrintQty] = @PrintQty WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(sql, new {
            Id = labelId,
            PrintQty = ev.PrintQty
        }, trx);
    }

    private async Task ApplyFieldFormulaSetEvent(IDbTransaction trx, int labelId, LabelFieldFormulaSetEvent ev) {
        const string query = @"SELECT ([Fields]) FROM [LabelFieldMaps] WHERE [Id] = @Id;";
        var json = await _connection.QuerySingleAsync<string>(query, new {
            Id = labelId
        });

        Dictionary<string, string>? fields;
        if (string.IsNullOrEmpty(json)) {
            fields = new();
        } else {
            fields = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
            if (fields is null) fields = new();
        }

        fields[ev.Field] = ev.Formula;

        json = JsonSerializer.Serialize(fields);

        const string update = @"SELECT [LabelFieldMaps] SET [Fields] = @Fields WHERE [Id] = @Id;";
        await _connection.ExecuteAsync(update, new {
            Id = labelId,
            Fields = json
        });
    }

}