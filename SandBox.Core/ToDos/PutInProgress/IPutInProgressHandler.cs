namespace SandBox.Core.ToDos.PutInProgress;

public interface IPutInProgressHandler
{
    Task<PutInProgressResult> Handle(Guid id);
}