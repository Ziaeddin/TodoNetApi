using TodoApi.Models;

namespace TodoApi.Services;

public interface ITodoService
{
    Task<List<TodoItem>> GetAllAsync();
    Task<TodoItem?> GetByIdAsync(int id);
    Task<TodoItem> CreateAsync(TodoItem item);
    Task<bool> UpdateAsync(int id, TodoItem item);
    Task<bool> DeleteAsync(int id);
}