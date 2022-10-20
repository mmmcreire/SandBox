using SandBox.Core.Tests.Common;
using SandBox.Core.ToDos.Create;

namespace SandBox.Core.Tests.Todos.Write;

public class DeleteTodoTest : BaseTest
{
    public DeleteTodoTest(Fixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Id_Not_Found()
    {
        var id = Guid.NewGuid();

        var result = await DeleteTodoHandler.Handle(id);

        Assert.Null(result);
        Assert.True(DomainValidator.HasNotFound());
    }

    [Fact]
    public async Task Should_Delete_Todo()
    {
        var description = "Test Description";
        var command = new CreateToDoCommand(description);
        var createdTodo = await CreateToDoHandler.Handle(command);
        var result = await DeleteTodoHandler.Handle(createdTodo.Id);

        Assert.Null(result);
        Assert.False(DomainValidator.HasFailValidation());
    }
}
