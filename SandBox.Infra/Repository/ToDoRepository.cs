using SandBox.Core.Ports;
using SandBox.Core.ToDos;

namespace SandBox.Infra.Repository;

public class ToDoRepository : IToDoRepository
{
    //private List<ToDo> todos = new List<ToDo>(); => before .net 6
    private static List<ToDo> ToDos = new() //after .net 6 we do not have to type new List<ToDo>() anymore, just new()
    {
        new ToDo("Todo 1"),
        new ToDo("Todo 2"),
        new ToDo("Todo 3"),
        new ToDo("Todo 4"),
        new ToDo("Todo 5"),
    };

    public Task<List<ToDo>> Get() =>
        Task.FromResult(ToDos);

    public Task<ToDo> GetById(Guid id) =>
        Task.FromResult(ToDos.FirstOrDefault(e => e.Id == id));

    public Task Save(ToDo todo)
    {
        ToDos.Add(todo);
        return Task.CompletedTask;
    }
}