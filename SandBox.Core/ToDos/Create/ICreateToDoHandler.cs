namespace SandBox.Core.ToDos.Create;

public interface ICreateToDoHandler
{
    Task<Tuple<CreateToDoResult, List<string>>> Handle(CreateToDoCommand command);
}