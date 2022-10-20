using SandBox.Core.Tests.Common;
using SandBox.Core.ToDos.Create;

namespace SandBox.Core.Tests.Todos.Write;

public class CreateTodoTest : BaseTest
{
    public CreateTodoTest(Fixture fixture) :
        base(fixture)
    { }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Less_Than_Two_Characters_Long()
    {
        var description = "aa";
        var command = new CreateToDoCommand(description);

        var result = await CreateToDoHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Null()
    {
        var command = new CreateToDoCommand(null);

        var result = await CreateToDoHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Save_Todo()
    {
        var description = "Test Description";
        var command = new CreateToDoCommand(description);

        var result = await CreateToDoHandler.Handle(command);
        var entity = await TodoRepository.GetById(result.Id);

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasFailValidation());
        Assert.NotNull(entity);
    }
}
