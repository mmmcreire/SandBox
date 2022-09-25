using SandBox.Core.Ports;

namespace SandBox.Core.ToDos.PutInProgress;

public class PutInProgressHandler : IPutInProgressHandler
{
    private readonly IToDoRepository _repository;

    public PutInProgressHandler(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<PutInProgressResult> Handle(Guid id)
    {
        var todo = await _repository.GetById(id);
        todo.PutInProgress();

        return null;
    }
}
