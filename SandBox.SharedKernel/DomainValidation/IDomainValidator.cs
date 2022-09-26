namespace SandBox.SharedKernel.DomainValidation;

public interface IDomainValidator
{
    void AddNotFound(string description);
    void AddFailValidations(List<Fail> fails);
    bool HasFailValidation();
    Dictionary<string, List<string>> GetFailValidations();
    bool HasNotFound();
    string GetNotFoundValidation();
}