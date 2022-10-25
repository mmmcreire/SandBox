using SandBox.Core.Tests.Common;

namespace SandBox.Core.Tests.Todos.Write;

public class PutInProgressTest : BaseTest
{
    public PutInProgressTest(Fixture fixture) : base(fixture)
    {
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
        var entity = await TodoGenerator.CreateNewTodo();
        await PutInProgressHandler.Handle(entity.Id);
        await MarkAsDoneHandler.Handle(entity.Id);

        var result = await PutInProgressHandler.Handle(entity.Id);

        Assert.Null(result);
        Assert.True(DomainValidator.HasFailValidation());
    }

    [Fact]
    public async Task Should_Put_Todo_In_Progress()
    {
        var entity = await TodoGenerator.CreateNewTodo();
        var result = await PutInProgressHandler.Handle(entity.Id);

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasFailValidation());
    }
}
