using FluentValidation;

namespace SandBox.Core.ToDos.Create;

public class CreateToDoValidator : AbstractValidator<CreateToDoCommand>
{
	public CreateToDoValidator()
	{
		RuleFor(e => e.Description)
			.NotNull().WithMessage("Description cannot be null")
			.NotEmpty().WithMessage("Description cannot be empty")
			.MinimumLength(3).WithMessage("Description should contains at least 3 characters")
			.MaximumLength(50).WithMessage("Description maximum length is 50");
	}
}