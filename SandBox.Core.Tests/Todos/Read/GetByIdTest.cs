using SandBox.Core.Tests.Common;

namespace SandBox.Core.Tests.Todos.Read;

public class GetByIdTest : BaseTest
{
    public GetByIdTest(Fixture fixture) : base(fixture)
    {
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
        var entity = await TodoGenerator.CreateNewTodo();
        var result = await GetByIdHandler.Handle(entity.Id);

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasNotFound());
    }

    [Fact]
    public async Task Should_Get_Correct_Todo()
    {
        var entity = await TodoGenerator.CreateNewTodo();

        var result = await GetByIdHandler.Handle(entity.Id);

        Assert.NotNull(result);
        Assert.False(DomainValidator.HasNotFound());
        Assert.Equal("aaa", result.Description);
    }
}
