namespace SandBox.Core.ToDos.UpdateDescription;

public record UpdateDescriptionResult(
    Guid Id, 
    string Description, 
    ToDoStatus Status
);
