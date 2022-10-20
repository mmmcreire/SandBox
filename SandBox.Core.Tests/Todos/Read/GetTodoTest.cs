using SandBox.Core.Tests.Common;
using SandBox.Core.ToDos.Create;

namespace SandBox.Core.Tests.Todos.Read;

public class GetTodoTest : BaseTest
{
    public GetTodoTest(Fixture fixture) : base(fixture)
    {
    }

    private async Task<CreateToDoResult> createNewTodo()
    {
        var command = new CreateToDoCommand("TodoTest");
        var todo = await CreateToDoHandler.Handle(command);
        return todo;
    }

    [Fact]
    public async Task Should_Get_No_Todo()
    {
        var result = await GetTodosHandler.Handle();

        Assert.Empty(result);
    }

    [Fact]
    public async Task Should_Get_One_Todo()
    {
        var command = createNewTodo();
        var result = await GetTodosHandler.Handle();

        Assert.NotEmpty(result);
        Assert.True(result.Count() == 1);
    }

    [Fact]
    public async Task Should_Get_Five_Todos()
    {
        var command1 = createNewTodo();
        var command2 = createNewTodo();
        var command3 = createNewTodo();
        var command4 = createNewTodo();
        var command5 = createNewTodo();
        var result = await GetTodosHandler.Handle();

        Assert.NotEmpty(result);
        Assert.True(result.Count() == 5);
    }
}
