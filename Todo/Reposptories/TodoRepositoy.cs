using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _context;

        public TodoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TodoItem?> GetByIdAsync(int id)
        {
            return await _context.TodoItems.FindAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _context.TodoItems
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> FindAsync(Expression<Func<TodoItem, bool>> predicate)
        {
            return await _context.TodoItems
                .Where(predicate)
                .ToListAsync();
        }

        public async Task<TodoItem> AddAsync(TodoItem entity)
        {
            _context.TodoItems.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<TodoItem> UpdateAsync(TodoItem entity)
        {
            _context.TodoItems.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var todo = await GetByIdAsync(id);
            if (todo == null) return false;

            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.TodoItems.AnyAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<TodoItem>> GetPendingTodosAsync()
        {
            return await _context.TodoItems
                .Where(t => !t.IsCompleted)
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TodoItem>> GetCompletedTodosAsync()
        {
            return await _context.TodoItems
                .Where(t => t.IsCompleted)
                .OrderByDescending(t => t.CompletedDate)
                .ToListAsync();
        }

        public async Task<bool> MarkAsCompletedAsync(int id)
        {
            var todo = await GetByIdAsync(id);
            if (todo == null) return false;

            todo.IsCompleted = true;
            todo.CompletedDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkAsPendingAsync(int id)
        {
            var todo = await GetByIdAsync(id);
            if (todo == null) return false;

            todo.IsCompleted = false;
            todo.CompletedDate = null;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}