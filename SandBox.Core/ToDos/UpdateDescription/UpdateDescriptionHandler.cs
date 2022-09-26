using FluentValidation;
using SandBox.Core.Ports;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.Core.ToDos.UpdateDescription;

public class UpdateDescriptionHandler : IUpdateDescriptionHandler
{
	private readonly IToDoRepository _repository;
	private readonly IDomainValidator _domainValidator;
	private readonly IValidator<UpdateDescriptionCommand> _validator;

	public UpdateDescriptionHandler(
		IToDoRepository repository,
		IDomainValidator domainValidator,
		IValidator<UpdateDescriptionCommand> validator
	)
	{
		_repository = repository;
		_domainValidator = domainValidator;
		_validator = validator;
	}

	public async Task<UpdateDescriptionResult> Handle(UpdateDescriptionCommand command)
	{
		var entity = await _repository.GetById(command.Id);

		if (entity == null)
		{
			_domainValidator.AddNotFound($"Todo with id {command.Id} not founded!");
			return null;
		}
		
		var validation = await _validator.ValidateAsync(command);

		if (!validation.IsValid)
		{
			var fails = validation.Errors
				.Select(e => new Fail(e.ErrorMessage, FailType.Validation, e.PropertyName))
				.ToList();
			
			_domainValidator.AddFailValidations(fails);
			return null;
		}
		
		entity.UpdateDescription(command.Description);
		await _repository.Update(entity);
		
		return new UpdateDescriptionResult(entity.Id, entity.Description, entity.Status);
	}
}
