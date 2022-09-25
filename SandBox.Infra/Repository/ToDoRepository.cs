using Microsoft.EntityFrameworkCore;
using SandBox.Core.Ports;
using SandBox.Core.ToDos;
using SandBox.Infra.Database;

namespace SandBox.Infra.Repository;

public class ToDoRepository : IToDoRepository
{
    private readonly DatabaseContext _context;
    private readonly DbSet<ToDo> _todos;

    public ToDoRepository(DatabaseContext context)
    {
        _context = context;
        _todos = context.Set<ToDo>();
    }

    public async Task<List<ToDo>> Get() =>
        await _todos.ToListAsync();

    public async Task<ToDo> GetById(Guid id) =>
        await _todos.FirstOrDefaultAsync(e => e.Id == id);

    public async Task Save(ToDo todo)
    {
        await _todos.AddAsync(todo);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        _todos.Remove(_todos.FirstOrDefault(e => e.Id == id));
        await _context.SaveChangesAsync();
    }

    public async Task MarkAsDone(ToDo todo)
    {
        var updatedTodo = _todos.FirstOrDefault(e => e.Id == todo.Id);
        updatedTodo.MarkAsDone();
        await _context.SaveChangesAsync();
    }
    public async Task PutInProgress(ToDo todo)
    {
        var updatedTodo = _todos.FirstOrDefault(e => e.Id == todo.Id);
        updatedTodo.PutInProgress();
        await _context.SaveChangesAsync();
    }

    public async Task UpdateDescription(ToDo todo)
    {
        var updatedTodo = _todos.FirstOrDefault(e => e.Id == todo.Id);
        updatedTodo.UpdateDescription(todo.Description);
        await _context.SaveChangesAsync();
    }
}