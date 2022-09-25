using SandBox.Core.Ports;

namespace SandBox.Core.ToDos.Delete;

public class DeleteTodoHandler : IDeleteTodoHandler
{
    private readonly IToDoRepository _repository;

    public DeleteTodoHandler(IToDoRepository todoRepository)
    {
        _repository = todoRepository;
    }

    public async Task<DeleteByIdResult> Handle(Guid id)
    {
        var todo = await _repository.GetById(id);

        if(todo is null)
        {
            return null;
        }
        await _repository.Delete(id);
        return null;
    }
}