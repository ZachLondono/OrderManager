using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Functions;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>  where TRequest : IRequest<TResponse> {
    
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {

        if (_validators.Any()) {
            var context = new ValidationContext<TRequest>(request);

            // Runs all of the validators for the given TRequest
            var validationResults = await Task.WhenAll(
                _validators.Select(v =>
                    v.ValidateAsync(context, cancellationToken)));

            // Collects any errors in the validation
            var failures = validationResults
                .Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
                throw new ValidationException(failures);
        }

        return await next();

    }

}
