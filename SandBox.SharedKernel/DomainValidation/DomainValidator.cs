namespace SandBox.SharedKernel.DomainValidation;

public class DomainValidator : IDomainValidator
{
    private readonly List<Fail> _fails = new();

    public void AddNotFound(string description) =>
        _fails.Add(new Fail(description, FailType.NotFound, "notfound"));

    public void AddFailValidations(List<Fail> fails) =>
        _fails.AddRange(fails);

    public bool HasFailValidation() =>
        _fails.Any(e => e.Type == FailType.Validation);

    public Dictionary<string, List<string>> GetFailValidations() =>
        _fails.Where(e => e.Type == FailType.Validation)
            .GroupBy(e => e.Field)
            .ToDictionary(
                key => key.Key,
                values => values
                    .Select(e => e.Description)
                    .ToList());

    public bool HasNotFound() =>
        _fails.Any(e => e.Type == FailType.NotFound);

    public string GetNotFoundValidation() =>
        _fails.First(e => e.Type == FailType.NotFound).Description;

    public void ClearValidations() =>
        _fails.Clear();
}