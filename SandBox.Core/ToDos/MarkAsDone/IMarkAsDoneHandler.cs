namespace SandBox.Core.ToDos.MarkAsDone;

public interface IMarkAsDoneHandler
{
    Task<MarkAsDoneResult> Handle(Guid id);
}
