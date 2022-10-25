using SandBox.Core.Ports;
using SandBox.Core.Tests.Common.Generators;
using SandBox.Core.ToDos.Create;
using SandBox.Core.ToDos.Delete;
using SandBox.Core.ToDos.Get;
using SandBox.Core.ToDos.GetById;
using SandBox.Core.ToDos.MarkAsDone;
using SandBox.Core.ToDos.PutInProgress;
using SandBox.Core.ToDos.UpdateDescription;
using SandBox.Infra.Database;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.Core.Tests.Common;

public class BaseTest : IClassFixture<Fixture>
{
    protected readonly ICreateToDoHandler CreateToDoHandler;
    protected readonly IDomainValidator DomainValidator;
    protected readonly IToDoRepository TodoRepository;
    protected readonly IMarkAsDoneHandler MarkAsDoneHandler;
    protected readonly IPutInProgressHandler PutInProgressHandler;
    protected readonly IUpdateDescriptionHandler UpdateDescriptionHandler;
    protected readonly IDeleteTodoHandler DeleteTodoHandler;
    protected readonly IGetToDosHandler GetTodosHandler;
    protected readonly IGetByIdHandler GetByIdHandler;
    protected readonly ITodoGenerator TodoGenerator;

    protected BaseTest(Fixture fixture)
    {
        CreateToDoHandler = fixture.Get<ICreateToDoHandler>();
        MarkAsDoneHandler = fixture.Get<IMarkAsDoneHandler>();
        PutInProgressHandler = fixture.Get<IPutInProgressHandler>();
        UpdateDescriptionHandler = fixture.Get<IUpdateDescriptionHandler>();
        DeleteTodoHandler = fixture.Get<IDeleteTodoHandler>();
        GetTodosHandler = fixture.Get<IGetToDosHandler>();
        GetByIdHandler = fixture.Get<IGetByIdHandler>();
        DomainValidator = fixture.Get<IDomainValidator>();
        TodoRepository = fixture.Get<IToDoRepository>();
        TodoGenerator = new TodoGenerator(TodoRepository);
        DomainValidator.ClearValidations();

        fixture.Get<DatabaseContext>().Database.EnsureDeleted();
    }
}