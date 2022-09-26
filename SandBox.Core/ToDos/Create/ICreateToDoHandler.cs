namespace SandBox.Core.ToDos.Create;

public interface ICreateToDoHandler
{
    Task<CreateToDoResult> Handle(CreateToDoCommand command);
}