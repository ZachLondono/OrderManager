using FluentValidation;
using MediatR;
using OrderManager.ApplicationCore.Infrastructure;
using System.Diagnostics;

namespace OrderManager.ApplicationCore.Features.ExcelPrint;

public class PrintExcelFile {

    public enum ProcessStatus {
        Success,
        Failed
    }

    public record ProcessResult(ProcessStatus Status, List<string> Errors);

    public record Command(string FilePath, string SheetName, string ExportPath) : IRequest<ProcessResult>;

    public class Validator : AbstractValidator<Command> {
        public Validator() {
            RuleFor(c => c.FilePath)
                .NotNull()
                .NotEmpty()
                .Must(File.Exists);

            RuleFor(c => c.SheetName)
                .NotNull()
                .NotEmpty();

            RuleFor(c => c.ExportPath)
                .NotNull()
                .NotEmpty();
        }
    }

    public class Handler : IRequestHandler<Command, ProcessResult> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public async Task<ProcessResult> Handle(Command request, CancellationToken cancellationToken) {

            string args = $"--FilePath \"{request.FilePath}\" --SheetName \"{request.SheetName}\" --ExportPath \"{request.ExportPath}\"";

            try {

                List<string> errors = new();

                // Check if the program writes an error to StdError
                Action<String> onStdErr = s => {
                    errors.Add(s);
                };

                var process = new Process {
                    StartInfo = {
                        FileName = _config.ExcelPrinterExecutable,
                        Arguments = args,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardOutput = false,
                        RedirectStandardError = true,
                    }
                };

                process.Start();

                // Read from std error
                Task.Run(() => ReadStream(process.StandardError, onStdErr));

                await Task.Run(() => process.WaitForExit());

                ProcessStatus status = errors.Any() ? ProcessStatus.Failed : ProcessStatus.Success;

                return new ProcessResult(status, errors);

            } catch (Exception e) {
                List<string> errors = new() { e.ToString() };
                return new ProcessResult(ProcessStatus.Failed, errors);
            }
        }

        private static void ReadStream(TextReader textReader, Action<String> callback) {
            while (true) {
                var line = textReader.ReadLine();
                if (line == null)
                    break;

                callback(line);
            }
        }

    }

}
