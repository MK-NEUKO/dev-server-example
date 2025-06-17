using UserManager.Application.Abstractions.Messaging;

namespace UserManager.Application.Todos.Get;

public sealed record GetTodosQuery(Guid UserId) : IQuery<List<TodoResponse>>;
