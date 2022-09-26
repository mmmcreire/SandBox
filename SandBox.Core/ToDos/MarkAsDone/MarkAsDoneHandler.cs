using SandBox.Core.Ports;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.Core.ToDos.MarkAsDone;

public class MarkAsDoneHandler : IMarkAsDoneHandler
{
    private readonly IToDoRepository _repository;
    private readonly IDomainValidator _domainValidator;

    public MarkAsDoneHandler(
        IToDoRepository repository,
        IDomainValidator domainValidator
    )
    {
        _repository = repository;
        _domainValidator = domainValidator;
    }

    public async Task<MarkAsDoneResult> Handle(Guid id)
    {
        var todo = await _repository.GetById(id);
        
        if(todo is null)
        {
            _domainValidator.AddNotFound($"Todo with id {id} not founded");
            return null;
        }

        if (todo.Status == ToDoStatus.Created)
        {
            var fails = new List<Fail>
            {
                new(
                    "Cannot mark todo as done if is not in progress", 
                    FailType.Validation, 
                    "status"
                )
            };
            
            _domainValidator.AddFailValidations(fails);
            return null;
        }
        
        todo.MarkAsDone();
        await _repository.Update(todo);
        return new MarkAsDoneResult(todo.Id, todo.Description, todo.Status);
    }
}
