using SandBox.Core.Ports;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.Core.ToDos.Delete;

public class DeleteTodoHandler : IDeleteTodoHandler
{
    private readonly IToDoRepository _repository;
    private readonly IDomainValidator _domainValidator;

    public DeleteTodoHandler(
        IToDoRepository todoRepository,
        IDomainValidator domainValidator
    )
    {
        _repository = todoRepository;
        _domainValidator = domainValidator;
    }

    public async Task<DeleteByIdResult> Handle(Guid id)
    {
        var todo = await _repository.GetById(id);

        if(todo is null)
        {
            _domainValidator.AddNotFound($"Todo with id {id} not found");
            return null;
        }

        await _repository.Delete(todo);
        return null;
    }
}