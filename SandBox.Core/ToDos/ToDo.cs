namespace SandBox.Core.ToDos;

public class ToDo
{
    public Guid Id { get; }
    public string Description { get; private set; }
    public ToDoStatus Status { get; private set; }

    private ToDo() { }

    public ToDo(string description)
    {
        if(string.IsNullOrEmpty(description))
            throw new ArgumentException("Description cannot be null or empty");

        Id = Guid.NewGuid();
        Description = description;
        Status = ToDoStatus.Created;
    }

    public void PutInProgress()
    {
        if(Status == ToDoStatus.Done)
            throw new ArgumentException("Todo is already Done");

        Status = ToDoStatus.InProgress;
    }

    public void MarkAsDone()
    {
        if(Status == ToDoStatus.Created)
            throw new ArgumentException("Status should be In Progress before change to Done");
        Status = ToDoStatus.Done;
    }

    public void UpdateDescription(string description)
    {
        if(string.IsNullOrEmpty(description))
            throw new ArgumentException("Description cannot be null or empty");

        Description = description;
    }
}