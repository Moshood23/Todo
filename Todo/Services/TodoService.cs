using TodoApp.Models;
using TodoApp.Repositories;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoItem?> GetTodoByIdAsync(int id)
        {
            return await _todoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _todoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetPendingTodosAsync()
        {
            return await _todoRepository.GetPendingTodosAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetCompletedTodosAsync()
        {
            return await _todoRepository.GetCompletedTodosAsync();
        }

        public async Task<TodoItem> CreateTodoAsync(TodoItem todoItem)
        {
            if (string.IsNullOrWhiteSpace(todoItem.Title))
                throw new ArgumentException("Title is required");

            if (todoItem.Title.Length > 100)
                throw new ArgumentException("Title cannot exceed 100 characters");

            return await _todoRepository.AddAsync(todoItem);
        }

        public async Task<TodoItem?> UpdateTodoAsync(TodoItem todoItem)
        {
            var existingTodo = await _todoRepository.GetByIdAsync(todoItem.Id);
            if (existingTodo == null) return null;

            if (string.IsNullOrWhiteSpace(todoItem.Title))
                throw new ArgumentException("Title is required");

            if (todoItem.Title.Length > 100)
                throw new ArgumentException("Title cannot exceed 100 characters");

            existingTodo.Title = todoItem.Title;
            existingTodo.Description = todoItem.Description;
            existingTodo.DueDate = todoItem.DueDate;

            return await _todoRepository.UpdateAsync(existingTodo);
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            return await _todoRepository.DeleteAsync(id);
        }

        public async Task<bool> ToggleTodoStatusAsync(int id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null) return false;

            if (todo.IsCompleted)
                return await _todoRepository.MarkAsPendingAsync(id);
            else
                return await _todoRepository.MarkAsCompletedAsync(id);
        }

        public async Task<bool> MarkTodoAsCompletedAsync(int id)
        {
            return await _todoRepository.MarkAsCompletedAsync(id);
        }

        public async Task<bool> MarkTodoAsPendingAsync(int id)
        {
            return await _todoRepository.MarkAsPendingAsync(id);
        }
    }
}