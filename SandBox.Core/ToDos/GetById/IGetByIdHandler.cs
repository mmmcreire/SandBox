namespace SandBox.Core.ToDos.GetById;

public interface IGetByIdHandler
{
    Task<GetByIdResult> Handle(Guid id);
}
