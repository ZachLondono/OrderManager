using Dapper;
using MediatR;
using FluentValidation;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;
using System.Data.OleDb;

namespace OrderManager.ApplicationCore.Features.Reports;

public class CreateReport {

    public record Command(string ReportName, string Template, string OutputDirectory, ReportType ReportType) : IRequest<Report?>;

    public class Validator : AbstractValidator<Command> {

        private readonly AppConfiguration _config;

        public Validator(AppConfiguration config) {
            _config = config;

            RuleFor(r => r.ReportName)
                .NotNull()
                .NotEmpty()
                .Must(IsNameUnique);

            RuleFor(r => r.Template)
                .NotNull()
                .NotEmpty()
                .Must(File.Exists)
                .WithMessage("Template must be a valid file");

            RuleFor(r => r.OutputDirectory)
                .NotNull()
                .NotEmpty()
                .Must(IsDirectory)
                .WithMessage("Must be valid directory to output files to");
        }

        private bool IsDirectory(string directory) => 
                            string.IsNullOrEmpty(Path.GetFileName(directory))
                            || Directory.Exists(directory)
                            || string.IsNullOrEmpty(directory);

        public bool IsNameUnique(string name) {
            const string query = "SELECT COUNT([Name]) FROM Reports WHERE [Name] = [@Name];";
            using var connection = new OleDbConnection(_config.OrderConnectionString);
            connection.Open();
            int count = connection.QuerySingle<int>(query, new { ReportName = name });
            connection.Close();
            return count == 0;
        }

    }

    internal class Handler : IRequestHandler<Command, Report?> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<Report?> Handle(Command request, CancellationToken cancellationToken) {

            string sql = "INSERT INTO Reports ([Name], [Template], [OutputDirectory], [ReportType]) VALUES ([@Name], [@Template], [@OutputDirectory], [@ReportType]);";

            string query = "SELECT * FROM Reports WHERE [Name] = [@Name];";

            using var connection = new OleDbConnection(_config.OrderConnectionString);

            connection.Open();

            int rowsAffected = await connection.ExecuteAsync(sql, request);
            
            Report? report = null;
            if (rowsAffected > 0) {
                report = await connection.QueryFirstOrDefaultAsync<Report>(query, new { request.ReportName });
            }

            connection.Close();

            return report;

        }

    }

}
