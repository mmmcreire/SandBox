using SandBox.Core.Ports;
using SandBox.Core.ToDos;

namespace SandBox.Core.Tests.Common.Generators;

public class TodoGenerator : ITodoGenerator
{
    private readonly IToDoRepository _repository;

    public TodoGenerator(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<ToDo> CreateNewTodo()
    {
        var entity = new ToDo("aaa");
        await _repository.Save(entity);
        return entity;
    }
}
