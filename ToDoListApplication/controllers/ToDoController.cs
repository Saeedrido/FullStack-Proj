using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoListApp.Model;
using ToDoListApp.Model.Enum;
using ToDoListApp.Services;

namespace ToDoListApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        // Get all tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            var todoItems = await _todoItemService.GetAllTasksAsync();
            return Ok(todoItems);
        }

        // Get task by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(int id)
        {
            var todoItem = await _todoItemService.GetTaskByIdAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        // Create new task
        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            // Validate the priority
            if (!Enum.IsDefined(typeof(Priority), todoItem.TaskPriority))
            {
                return BadRequest("Invalid priority value.");
            }

            await _todoItemService.AddTaskAsync(todoItem);
            return CreatedAtAction("GetTodoItem", new { id = todoItem.Id }, todoItem);
        }


        // Update task
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }
            await _todoItemService.UpdateTaskAsync(todoItem);
            return NoContent();
        }

        // Delete task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(int id)
        {
            await _todoItemService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
