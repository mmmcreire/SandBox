using SandBox.Core.Ports;

namespace SandBox.Core.ToDos.Get;

public class GetToDosHandler : IGetToDosHandler
{
    private readonly IToDoRepository _repository;

    public GetToDosHandler(IToDoRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetToDosResult>> Handle()
    {
        var todos = await _repository.Get();
        return todos.Select(e => new GetToDosResult(e.Id, e.Description, e.Status)).ToList();
    }
}
