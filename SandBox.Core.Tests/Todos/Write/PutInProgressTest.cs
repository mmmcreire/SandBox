using SandBox.Core.Tests.Common;
using SandBox.Core.ToDos.Create;

namespace SandBox.Core.Tests.Todos.Write;

public class PutInProgressTest : BaseTest
{
    public PutInProgressTest(Fixture fixture) : base(fixture)
    {
    }

    private async Task<CreateToDoResult> createNewTodo()
    {
        var command = new CreateToDoCommand("TodoTest");
        var todo = await CreateToDoHandler.Handle(command);
        return todo;
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Id_Not_Found()
    {
        var id = Guid.NewGuid();

        var result = await PutInProgressHandler.Handle(id);

        Assert.Null(result);
        Assert.True(DomainValidator.HasNotFound());
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Todo_Is_Done()
    {
        var newTodo = createNewTodo();
        var inProgressTodo = await PutInProgressHandler.Handle(newTodo.Result.Id);
        var doneTodo = await MarkAsDoneHandler.Handle(inProgressTodo.Id);

        var result = await PutInProgressHandler.Handle(doneTodo.Id);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Put_Todo_In_Progress()
    {
        var newTodo = createNewTodo();
        var result = await PutInProgressHandler.Handle(newTodo.Result.Id);

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasFailValidation());
    }
}
