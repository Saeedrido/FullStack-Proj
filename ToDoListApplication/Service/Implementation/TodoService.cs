using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Model;
using ToDoListApp.Repositories;

namespace ToDoListApp.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemService(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllTasksAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem> GetTaskByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddTaskAsync(TodoItem todoItem)
        {
            await _repository.AddAsync(todoItem);
        }

        public async Task UpdateTaskAsync(TodoItem todoItem)
        {
            await _repository.UpdateAsync(todoItem);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
