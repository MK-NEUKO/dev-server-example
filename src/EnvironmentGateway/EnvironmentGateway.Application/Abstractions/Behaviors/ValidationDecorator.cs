using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Application.Exceptions;
using EnvironmentGateway.Domain.Abstractions;
using FluentValidation;
using FluentValidation.Results;

namespace EnvironmentGateway.Application.Abstractions.Behaviors;

internal static class ValidationDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var validatorErrors = Validate(command, validators);

            if (validatorErrors.Any())
            {
                throw new Exceptions.ValidationException(validatorErrors);
            }

            return await innerHandler.Handle(command, cancellationToken);
        }
    }
    
    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        IEnumerable<IValidator<TCommand>> validators)
        : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var validatorErrors = Validate(command, validators);

            if (validatorErrors.Any())
            {
                throw new Exceptions.ValidationException(validatorErrors);
            }

            return await innerHandler.Handle(command, cancellationToken);
        }
    }

    private static List<ValidationError> Validate<TCommand>(
        TCommand command,
        IEnumerable<IValidator<TCommand>> validators)
    {
        if (!validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TCommand>(command);

        var validatorErrors = validators
            .Select(validator => validator.Validate(context))
            .Where(validationResult => validationResult.Errors.Any())
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationError(
                validationFailure.PropertyName,
                validationFailure.ErrorMessage))
            .ToList();

        return validatorErrors;
    }
}
    
    