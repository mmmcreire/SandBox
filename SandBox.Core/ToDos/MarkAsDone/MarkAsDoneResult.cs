namespace SandBox.Core.ToDos.MarkAsDone;

public record MarkAsDoneResult(Guid Id, String Description, ToDoStatus Status);

