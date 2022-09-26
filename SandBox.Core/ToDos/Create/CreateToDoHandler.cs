using FluentValidation;
using SandBox.Core.Ports;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.Core.ToDos.Create;

public class CreateToDoHandler : ICreateToDoHandler
{
	private readonly IToDoRepository _repository;
	private readonly IDomainValidator _domainValidator;
	private readonly IValidator<CreateToDoCommand> _validator;

	public CreateToDoHandler(
		IToDoRepository repository,
		IDomainValidator domainValidator,
		IValidator<CreateToDoCommand> validator
	)
	{
		_repository = repository;
		_domainValidator = domainValidator;
		_validator = validator;
	}

	public async Task<CreateToDoResult> Handle(CreateToDoCommand command)
	{
		var validation = await _validator.ValidateAsync(command);

		if (!validation.IsValid)
		{
			var fails = validation.Errors
				.Select(e => new Fail(e.ErrorMessage, FailType.Validation, e.PropertyName))
				.ToList();
			
			_domainValidator.AddFailValidations(fails);
			return null;
		}

		var todo = new ToDo(command.Description);
		await _repository.Save(todo);
		return new CreateToDoResult(todo.Id, todo.Description, todo.Status);
	}
}
