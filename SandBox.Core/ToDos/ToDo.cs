namespace SandBox.Core.ToDos;

public class ToDo
{
    public Guid Id { get; }
    public string Description { get; private set; }
    public ToDoStatus Status { get; private set; }

    public ToDo(string description)
    {
        if(string.IsNullOrEmpty(description))
            throw new ArgumentException("Description cannot be null or empty");

        Id = Guid.NewGuid();
        Description = description;
        Status = ToDoStatus.Created;
    }
}