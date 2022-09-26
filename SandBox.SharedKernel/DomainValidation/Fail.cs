namespace SandBox.SharedKernel.DomainValidation;

public record Fail(
    string Description,
    FailType Type,
    string Field
);