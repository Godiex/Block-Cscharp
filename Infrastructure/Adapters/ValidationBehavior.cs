using FluentValidation;
using MediatR;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Infrastructure.Adapters;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();
        var context = new ValidationContext<TRequest>(request);

        var failures = _validators
            .SelectMany(x => x.Validate(context).Errors)
            .ToLookup(_ => _.PropertyName, _ => _.ErrorMessage);

        if (!failures.Any()) return await next();
        var failureMessages = string.Join(
            Environment.NewLine,
            failures.Select(f => f.ElementAt(0))
        );

    throw new ValidationException(failureMessages);


    }
}