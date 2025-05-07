using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Model;

namespace ToDoListApp.Services
{
    public interface ITodoItemService
    {
        Task<IEnumerable<TodoItem>> GetAllTasksAsync();
        Task<TodoItem> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TodoItem todoItem);
        Task UpdateTaskAsync(TodoItem todoItem);
        Task DeleteTaskAsync(int id);
    }
}
