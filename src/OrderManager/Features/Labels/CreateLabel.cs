using Dapper;
using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Labels;

public class CreateLabel {

    public record Command(string Name, string TemplatePath, LabelType Type, IEnumerable<LabelField> Fields) : IRequest<Label?>;

    public class Validator : AbstractValidator<Command> {
        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(c => c.Name)
                .NotEmpty()
                .NotNull()
                .Must(IsNameUnique);

        }

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT([Name]) FROM [Labels] WHERE [Name] = [@Name];";
            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { Name = name });
            connection.Close();
            return count == 0;
        }
    }

    internal class Handler : IRequestHandler<Command, Label?> {
        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Label?> Handle(Command request, CancellationToken cancellationToken) {
            
            string sql = @"INSERT INTO [Labels]
                            ([Name], [TemplatePath], [Type])
                            VALUES ([@Name], [@TemplatePath], [@Type]);";

            string fieldSql = @"INSERT INTO [LabelFields]
                                ([LabelId], [Key], [Value])
                                VALUES ([@LabelId], [@Key], [@Value]);";

            string query = "SELECT * FROM [Labels] WHERE [Name] = [@Name];";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            int rows = await connection.ExecuteAsync(sql, request);

            Label? newLabel = null;
            if (rows > 0) {
                newLabel = await connection.QueryFirstOrDefaultAsync<Label>(query, request);

                newLabel.Fields = request.Fields;

                foreach (var field in request.Fields) {
                    await connection.ExecuteAsync(fieldSql, new { 
                        LabelId = newLabel.Id,
                        Key = field.Key,
                        Value = field.Value
                    });
                }

            }

            connection.Close();

            return newLabel;

        }
    }

}
