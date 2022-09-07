using FluentValidation;
using SandBox.Core.Ports;

namespace SandBox.Core.ToDos.Create;

public class CreateToDoHandler : ICreateToDoHandler
{
	private readonly IToDoRepository _repository;
	private readonly IValidator<CreateToDoCommand> _validator;

	public CreateToDoHandler(
		IToDoRepository repository,
		IValidator<CreateToDoCommand> validator
		)
	{
		_repository = repository;
		_validator = validator;
	}

	public async Task<Tuple<CreateToDoResult, List<string>>> Handle(CreateToDoCommand command)
	{
		var validation = await _validator.ValidateAsync(command);
		if(!validation.IsValid)
		{
			var errors = validation.Errors.Select(e => e.ErrorMessage).ToList();
			return new Tuple<CreateToDoResult, List<string>>(null, errors);
		}

		var todo = new ToDo(command.Description);
		await _repository.Save(todo);
		var result = new CreateToDoResult(todo.Id, todo.Description, todo.Status);

		return new Tuple<CreateToDoResult, List<string>>(result, null);
	}
}
