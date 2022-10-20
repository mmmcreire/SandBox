using SandBox.Core.Ports;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.Core.ToDos.PutInProgress;

public class PutInProgressHandler : IPutInProgressHandler
{
    private readonly IToDoRepository _repository;
    private readonly IDomainValidator _domainValidator;

    public PutInProgressHandler(
        IToDoRepository repository,
        IDomainValidator domainValidator
    )
    {
        _repository = repository;
        _domainValidator = domainValidator;
    }

    public async Task<PutInProgressResult> Handle(Guid id)
    {
        var todo = await _repository.GetById(id);

        if(todo is null)
        {
            _domainValidator.AddNotFound($"Todo with id {id} not found");
            return null;
        }

        if(todo.Status == ToDoStatus.Done)
        {
            var fails = new List<Fail>
            {
                new(
                    "Cannot put todo in progress it his already done!",
                    FailType.Validation,
                    "status"
                )
            };

            _domainValidator.AddFailValidations(fails);
            return null;
        }

        todo.PutInProgress();
        await _repository.Update(todo);

        return new PutInProgressResult(todo.Id, todo.Description, todo.Status);
    }
}
