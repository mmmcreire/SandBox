namespace SandBox.Core.ToDos
{
    public class ToDo
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public ToDoStatus Status { get; set; }
    }
}