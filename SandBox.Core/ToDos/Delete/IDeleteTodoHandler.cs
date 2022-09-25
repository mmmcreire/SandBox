namespace SandBox.Core.ToDos.Delete;

public interface IDeleteTodoHandler
{
    Task<DeleteByIdResult> Handle(Guid id);
}
