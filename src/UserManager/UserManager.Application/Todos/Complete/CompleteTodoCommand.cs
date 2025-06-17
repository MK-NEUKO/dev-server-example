using UserManager.Application.Abstractions.Messaging;

namespace UserManager.Application.Todos.Complete;

public sealed record CompleteTodoCommand(Guid TodoItemId) : ICommand;
