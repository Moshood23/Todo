using TodoApp.Models;

namespace TodoApp.Repositories
{
    public interface ITodoRepository : IRepository<TodoItem>
    {
        Task<IEnumerable<TodoItem>> GetPendingTodosAsync();
        Task<IEnumerable<TodoItem>> GetCompletedTodosAsync();
        Task<bool> MarkAsCompletedAsync(int id);
        Task<bool> MarkAsPendingAsync(int id);
    }
}