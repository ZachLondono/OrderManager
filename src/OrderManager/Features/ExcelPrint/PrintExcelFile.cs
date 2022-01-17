using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Infrastructure;
using System.Diagnostics;

namespace OrderManager.ApplicationCore.Features.ExcelPrint;

public class PrintExcelFile {

    public record Command(string FilePath, string SheetName, string Printer, string ExportFile) : IRequest<bool>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(c => c.FilePath)
                .NotNull()
                .NotEmpty()
                .Must(File.Exists);

            RuleFor(c => c.SheetName)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.Printer)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.ExportFile)
                .NotNull();
        }
    }

    public class Handler : IRequestHandler<Command, bool> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public Task<bool> Handle(Command request, CancellationToken cancellationToken) {
            try {
                Process.Start(_config.ExcelPrinterExecutable, $"--file {request.FilePath} --sheet {request.SheetName} {(string.IsNullOrEmpty(request.ExportFile) ? "" : $"--export {request.ExportFile}")} --printer {request.Printer}");
                return Task.FromResult(true);
            } catch {
                return Task.FromResult(false);
            }
        }
    }

}
