using UserManager.Application.Abstractions.Messaging;

namespace UserManager.Application.Todos.GetById;

public sealed record GetTodoByIdQuery(Guid TodoItemId) : IQuery<TodoResponse>;
