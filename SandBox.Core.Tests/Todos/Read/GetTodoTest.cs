using SandBox.Core.Tests.Common;

namespace SandBox.Core.Tests.Todos.Read;

public class GetTodoTest : BaseTest
{
    public GetTodoTest(Fixture fixture) : base(fixture)
    {
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
        await TodoGenerator.CreateNewTodo();
        var result = await GetTodosHandler.Handle();

        Assert.NotEmpty(result);
        Assert.True(result.Count() == 1);
    }
}
