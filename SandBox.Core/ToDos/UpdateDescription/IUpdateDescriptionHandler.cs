namespace SandBox.Core.ToDos.UpdateDescription;

public interface IUpdateDescriptionHandler
{
    Task<UpdateDescriptionResult> Handle(UpdateDescriptionCommand command);
}
