using UserManager.Application.Abstractions.Messaging;

namespace UserManager.Application.Todos.Delete;

public sealed record DeleteTodoCommand(Guid TodoItemId) : ICommand;
