namespace SandBox.Core.ToDos.Get;

public record GetToDosResult(Guid Id, string Description, ToDoStatus Status);
