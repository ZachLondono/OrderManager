using MediatR;
using OrderManager.ApplicationCore.Domain;
using OrderManager.ApplicationCore.Infrastructure;

namespace OrderManager.ApplicationCore.Features.Scripts;

public class GetScripts {

    public record Query() : IRequest<IEnumerable<Script>>;

    public class Handler : IRequestHandler<Query, IEnumerable<Script>> {

        private readonly AppConfiguration _config;

        public Handler(AppConfiguration config) {
            _config = config;
        }

        public Task<IEnumerable<Script>> Handle(Query request, CancellationToken cancellationToken) {

            string directory = _config?.ScriptDirectory ?? "";

            bool isDirectory = string.IsNullOrEmpty(Path.GetFileName(directory))
                            || Directory.Exists(directory)
                            || string.IsNullOrEmpty(directory);

            if (!isDirectory)
                throw new InvalidDataException($"Provided path not a directory '{directory}'");

            string[] files = Directory.GetFiles(directory);
            return Task.Run(() => {
                IList<Script> scripts = new List<Script>();
                foreach (string file in files) {
                    if (Path.GetExtension(file) != ".py") continue;
                    scripts.Add(new Script() {
                        Name = Path.GetFileNameWithoutExtension(file),
                        Source = Path.Combine(directory, file)
                    });
                }

                return (IEnumerable<Script>) scripts;
            });

        }
    }

}
