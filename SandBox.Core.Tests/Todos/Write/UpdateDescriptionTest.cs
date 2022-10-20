using SandBox.Core.Tests.Common;
using SandBox.Core.ToDos.Create;
using SandBox.Core.ToDos.UpdateDescription;

namespace SandBox.Core.Tests.Todos.Write;

public class UpdateDescriptionTest : BaseTest
{
    public UpdateDescriptionTest(Fixture fixture) : base(fixture)
    {
    }

    private async Task<CreateToDoResult> createNewTodo()
    {
        var command = new CreateToDoCommand("TodoTest");
        var todo = await CreateToDoHandler.Handle(command);
        return todo;
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Less_Than_Two_Characters_Long()
    {
        var descriptionLessThanTwoCharacters = "a";
        var newTodo = createNewTodo();
        var command = new UpdateDescriptionCommand(newTodo.Result.Id, descriptionLessThanTwoCharacters);
        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Null()
    {
        var newTodo = createNewTodo();
        var command = new UpdateDescriptionCommand(newTodo.Result.Id, null);

        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Empty()
    {
        var newTodo = createNewTodo();
        var command = new UpdateDescriptionCommand(newTodo.Result.Id, "");

        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Bigger_Than_Fifty_Characters()
    {
        var descriptionMoreThanFiftyCharacters = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        var newTodo = createNewTodo();
        var command = new UpdateDescriptionCommand(newTodo.Result.Id, descriptionMoreThanFiftyCharacters);

        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Update_Description()
    {
        var description = "Update Description Test";
        var newTodo = createNewTodo();
        var command = new UpdateDescriptionCommand(newTodo.Result.Id, description);

        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasFailValidation());
    }
}
