using SandBox.Core.Ports;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.Core.ToDos.GetById;

public class GetByIdHandler : IGetByIdHandler
{
    private readonly IToDoRepository _repository;
    private readonly IDomainValidator _domainValidator;

    public GetByIdHandler(
        IToDoRepository repository,
        IDomainValidator domainValidator
    )
    {
        _repository = repository;
        _domainValidator = domainValidator;
    }

    public async Task<GetByIdResult> Handle(Guid id)
    {
        var todo = await _repository.GetById(id);

        if(todo is null)
        {
            _domainValidator.AddNotFound($"Todo with id {id} not found");
            return null;
        }

        return new GetByIdResult(todo.Description, todo.Status);
    }
}