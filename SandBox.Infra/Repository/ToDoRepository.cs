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
}