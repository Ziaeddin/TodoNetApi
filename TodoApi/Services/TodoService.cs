using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Services;

public class TodoService : ITodoService
{
    private readonly AppDbContext _db;

    public TodoService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<TodoItem>> GetAllAsync()
    {
        return await _db.Todos.ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _db.Todos.FindAsync(id);
    }

    public async Task<TodoItem> CreateAsync(TodoItem item)
    {
        _db.Todos.Add(item);
        await _db.SaveChangesAsync();
        return item;
    }

    public async Task<bool> UpdateAsync(int id, TodoItem item)
    {
        if (id != item.Id) return false;

        var existing = await _db.Todos.FindAsync(id);
        if (existing == null) return false;

        existing.Title = item.Title;
        existing.IsCompleted = item.IsCompleted;
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _db.Todos.FindAsync(id);
        if (item == null) return false;

        _db.Todos.Remove(item);
        await _db.SaveChangesAsync();
        return true;
    }
}
