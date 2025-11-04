using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<TodoItem?> GetTodoByIdAsync(int id);
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task<IEnumerable<TodoItem>> GetPendingTodosAsync();
        Task<IEnumerable<TodoItem>> GetCompletedTodosAsync();
        Task<TodoItem> CreateTodoAsync(TodoItem todoItem);
        Task<TodoItem?> UpdateTodoAsync(TodoItem todoItem);
        Task<bool> DeleteTodoAsync(int id);
        Task<bool> ToggleTodoStatusAsync(int id);
        Task<bool> MarkTodoAsCompletedAsync(int id);
        Task<bool> MarkTodoAsPendingAsync(int id);
    }
}