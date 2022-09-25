namespace SandBox.Core.ToDos.UpdateDescription;

public interface IUpdateDescriptionHandler
{
    Task<List<UpdateDescriptionResult>> Handle(Guid id, string Description);
}
