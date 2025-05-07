using ToDoListApp.Model.Enum;

namespace ToDoListApp.Model;
public class TodoItem
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Date { get; set; }
    public string? Time { get; set; }
    public Priority TaskPriority { get; set; }
    public bool IsCompleted { get; set; }


}
