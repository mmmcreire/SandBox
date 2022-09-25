using SandBox.Core.Ports;

namespace SandBox.Core.ToDos.UpdateDescription;

public class UpdateDescriptionHandler : IUpdateDescriptionHandler
{
	private readonly IToDoRepository _repository;

	public UpdateDescriptionHandler(IToDoRepository repository)
	{
		_repository = repository;
	}

	public async Task<List<UpdateDescriptionResult>> Handle(Guid id, string description)
	{
		var updatedTodo = await _repository.GetById(id);
		updatedTodo.UpdateDescription(description);

		return null;
	}
}
