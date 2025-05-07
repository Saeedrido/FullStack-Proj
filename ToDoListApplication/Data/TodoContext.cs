// Data/TodoContext.cs
using Microsoft.EntityFrameworkCore;
using ToDoListApp.Model;

namespace ToDoListApp.Data;

public class TodoContext : DbContext
{
    public TodoContext(DbContextOptions<TodoContext> options) : base(options) { }

    public DbSet<TodoItem> TodoItems { get; set; }
}
