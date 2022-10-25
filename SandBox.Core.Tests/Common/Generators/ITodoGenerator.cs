using SandBox.Core.ToDos;

namespace SandBox.Core.Tests.Common.Generators
{
    public interface ITodoGenerator
    {
        Task<ToDo> CreateNewTodo();
    }
}