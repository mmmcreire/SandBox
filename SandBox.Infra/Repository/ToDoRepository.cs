using SandBox.Core.Ports;
using SandBox.Core.ToDos;

namespace SandBox.Infra.Repository;

public class ToDoRepository : IToDoRepository
{
    //private List<ToDo> todos = new List<ToDo>(); => before .net 6
    private static List<ToDo> ToDos = new() //after .net 6 we do not have to type new List<ToDo>() anymore, just new()
    {
        new ToDo() { Id = Guid.NewGuid(), Description = "Todo 1", Status = ToDoStatus.Created },
        new ToDo() { Id = Guid.NewGuid(), Description = "Todo 2", Status = ToDoStatus.Created },
        new ToDo() { Id = Guid.NewGuid(), Description = "Todo 3", Status = ToDoStatus.InProgress },
        new ToDo() { Id = Guid.NewGuid(), Description = "Todo 4", Status = ToDoStatus.Done },
        new ToDo() { Id = Guid.NewGuid(), Description = "Todo 5", Status = ToDoStatus.Done },
    };


    public Task<List<ToDo>> Get() =>
        Task.FromResult(ToDos);

    public Task<ToDo> GetById(Guid id) =>
        Task.FromResult(ToDos.FirstOrDefault(e => e.Id == id));
}