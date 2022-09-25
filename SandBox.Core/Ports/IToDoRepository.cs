using SandBox.Core.ToDos;

namespace SandBox.Core.Ports;

public interface IToDoRepository
{
    Task<List<ToDo>> Get();
    Task<ToDo> GetById(Guid id);
    Task Save(ToDo todo);
    Task Delete(Guid id);
    Task UpdateDescription(ToDo todo);
    Task PutInProgress(ToDo todo);
    Task MarkAsDone(ToDo todo);
}
