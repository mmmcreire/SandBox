using SandBox.Core.Ports;

namespace SandBox.Core.ToDos.MarkAsDone;

public class MarkAsDoneHandler : IMarkAsDoneHandler
{
    private readonly IToDoRepository _repository;

    public MarkAsDoneHandler(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<MarkAsDoneResult> Handle(Guid id)
    {
        var todo = await _repository.GetById(id);
        todo.MarkAsDone();

        return null;
    }
}
