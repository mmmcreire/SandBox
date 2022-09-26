using FluentValidation;
using SandBox.Core.ToDos.Create;

namespace SandBox.Core.ToDos.UpdateDescription;

public class UpdateDescriptionValidator : AbstractValidator<UpdateDescriptionCommand>
{
	public UpdateDescriptionValidator()
	{
		RuleFor(e => e.Description)
			.NotNull().WithMessage("Description cannot be null")
			.NotEmpty().WithMessage("Description cannot be empty")
			.MinimumLength(3).WithMessage("Description should contains at least 3 characters")
			.MaximumLength(50).WithMessage("Description maximum length is 50");
	}
}