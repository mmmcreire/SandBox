using SandBox.Core.Tests.Common;
using SandBox.Core.ToDos.Create;

namespace SandBox.Core.Tests.Todos.Write;

public class MarkAsDoneTest : BaseTest
{
	public MarkAsDoneTest(Fixture fixture) :
		base(fixture)
	{ }

	[Fact]
	public async Task Should_Add_Fail_Validation_If_Id_Not_Found()
	{
		var id = Guid.NewGuid();

		var result = await MarkAsDoneHandler.Handle(id);

		Assert.Null(result);
		Assert.True(DomainValidator.HasNotFound());
	}

	[Fact]
	public async Task Should_Add_Fail_Validation_If_Todo_Is_Not_In_Progress()
	{
		var description = "Test";
		var command = new CreateToDoCommand(description);
		var todo = await CreateToDoHandler.Handle(command);

		var result = await MarkAsDoneHandler.Handle(todo.Id);

		Assert.Null(result);
		Assert.True(DomainValidator.HasFailValidation());
	}

	[Fact]
	public async Task Should_Mark_Todo_As_Done()
	{
		var description = "Test";
		var command = new CreateToDoCommand(description);
		var createdTodo = await CreateToDoHandler.Handle(command);
		var todo = await PutInProgressHandler.Handle(createdTodo.Id);

		var result = await MarkAsDoneHandler.Handle(todo.Id);

		Assert.NotNull(result);
		Assert.False(DomainValidator.HasFailValidation());
	}
}
