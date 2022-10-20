using SandBox.Core.Tests.Common;
using SandBox.Core.ToDos.Create;

namespace SandBox.Core.Tests.Todos.Read;

public class GetByIdTest : BaseTest
{
    public GetByIdTest(Fixture fixture) : base(fixture)
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

        var result = await GetByIdHandler.Handle(id);

        Assert.Null(result);
        Assert.True(DomainValidator.HasNotFound());
    }

    [Fact]
    public async Task Should_Get_One_Todo()
    {
        var command = createNewTodo();
        var result = await GetByIdHandler.Handle(command.Result.Id);

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasNotFound());
    }

    [Fact]
    public async Task Should_Get_Correct_Todo()
    {
        var command = createNewTodo();

        var result = await GetByIdHandler.Handle(command.Result.Id);
        var descriptionResult = command.Result.Description;

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasNotFound());
        Assert.Equal("TodoTest", result.Description);
    }
}
