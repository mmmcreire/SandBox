namespace SandBox.Core.ToDos.UpdateDescription;

public record UpdateDescriptionCommand(
    Guid Id,
    string Description
);