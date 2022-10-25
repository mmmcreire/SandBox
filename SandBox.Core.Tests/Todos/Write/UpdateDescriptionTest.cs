using SandBox.Core.Tests.Common;
using SandBox.Core.ToDos.UpdateDescription;

namespace SandBox.Core.Tests.Todos.Write;

public class UpdateDescriptionTest : BaseTest
{
    public UpdateDescriptionTest(Fixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Less_Than_Two_Characters_Long()
    {
        var descriptionLessThanTwoCharacters = "a";
        var entity = await TodoGenerator.CreateNewTodo();
        var command = new UpdateDescriptionCommand(entity.Id, descriptionLessThanTwoCharacters);
        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Null()
    {
        var entity = await TodoGenerator.CreateNewTodo();
        var command = new UpdateDescriptionCommand(entity.Id, null);

        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Empty()
    {
        var entity = await TodoGenerator.CreateNewTodo();
        var command = new UpdateDescriptionCommand(entity.Id, "");

        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Add_Fail_Validation_If_Description_Is_Bigger_Than_Fifty_Characters()
    {
        var descriptionMoreThanFiftyCharacters = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        var entity = await TodoGenerator.CreateNewTodo();
        var command = new UpdateDescriptionCommand(entity.Id, descriptionMoreThanFiftyCharacters);

        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Update_Description()
    {
        var description = "Update Description Test";
        var entity = await TodoGenerator.CreateNewTodo();
        var command = new UpdateDescriptionCommand(entity.Id, description);

        var result = await UpdateDescriptionHandler.Handle(command);

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasFailValidation());
    }
}
