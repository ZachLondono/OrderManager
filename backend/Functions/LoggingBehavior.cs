using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Functions;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {

    private readonly ILogger<LoggingBehavior<TRequest,TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {

        _logger.LogInformation("Handling request of type: '{RequestType}'", typeof(TRequest));
        var response = await next();
        _logger.LogInformation("Request returned response: '{Response}'", response);

        return response;

    }
}
