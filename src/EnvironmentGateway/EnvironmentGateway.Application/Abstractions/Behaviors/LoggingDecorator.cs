using EnvironmentGateway.Application.Abstractions.Messaging;
using EnvironmentGateway.Domain.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace EnvironmentGateway.Application.Abstractions.Behaviors;

internal static class LoggingDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<CommandHandler<TCommand, TResponse>> logger) 
        : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var requestName = typeof(TCommand).Name;
            
            try
            {
                logger.LogInformation("Processing request {RequestName}", requestName);

                var result = await innerHandler.Handle(command, cancellationToken);

                if (result.IsSuccess)
                {
                    logger.LogInformation("Request {Request} processed successfully", requestName);
                }
                else
                {
                    using (LogContext.PushProperty("Error", result.Error, true))
                    {
                        logger.LogError("Request {Request} processed with error", requestName);
                    }
                }

                return result;
            }
#pragma warning disable S2139
            catch (Exception exception)
#pragma warning restore S2139
            {
                logger.LogError(exception, "Request {Request} processing failed", requestName);

                throw;
            }
        }
    }
    
    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        ILogger<CommandBaseHandler<TCommand>> logger) 
        : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var requestName = typeof(TCommand).Name;
            
            try
            {
                logger.LogInformation("Processing request {RequestName}", requestName);

                var result = await innerHandler.Handle(command, cancellationToken);

                if (result.IsSuccess)
                {
                    logger.LogInformation("Request {Request} processed successfully", requestName);
                }
                else
                {
                    using (LogContext.PushProperty("Error", result.Error, true))
                    {
                        logger.LogError("Request {Request} processed with error", requestName);
                    }
                }

                return result;
            }
#pragma warning disable S2139
            catch (Exception exception)
#pragma warning restore S2139
            {
                logger.LogError(exception, "Request {Request} processing failed", requestName);

                throw;
            }
        }
    }
    
    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> innerHandler,
        ILogger<QueryHandler<TQuery, TResponse>> logger) 
        : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            var requestName = typeof(TQuery).Name;
            
            try
            {
                logger.LogInformation("Processing request {RequestName}", requestName);

                var result = await innerHandler.Handle(query, cancellationToken);

                if (result.IsSuccess)
                {
                    logger.LogInformation("Request {Request} processed successfully", requestName);
                }
                else
                {
                    using (LogContext.PushProperty("Error", result.Error, true))
                    {
                        logger.LogError("Request {Request} processed with error", requestName);
                    }
                }

                return result;
            }
#pragma warning disable S2139
            catch (Exception exception)
#pragma warning restore S2139
            {
                logger.LogError(exception, "Request {Request} processing failed", requestName);

                throw;
            }
        }
    }
}