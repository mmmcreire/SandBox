using SandBox.Core.Ports;
using SandBox.Core.ToDos.Create;
using SandBox.Infra.Database;
using SandBox.SharedKernel.DomainValidation;

namespace SandBox.Core.Tests.Common;

public class BaseTest : IClassFixture<Fixture>
{
    protected readonly ICreateToDoHandler CreateToDoHandler;
    protected readonly IDomainValidator DomainValidator;
    protected readonly IToDoRepository TodoRepository;

    protected BaseTest(Fixture fixture)
    {
        CreateToDoHandler = fixture.Get<ICreateToDoHandler>();
        DomainValidator = fixture.Get<IDomainValidator>();
        TodoRepository = fixture.Get<IToDoRepository>();
        DomainValidator.ClearValidations();

        fixture.Get<DatabaseContext>().Database.EnsureDeleted();
    }
}