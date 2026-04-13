// Controllers/TodosController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly AppDbContext _db;

    public TodosController(AppDbContext db)
    {
        _db = db;
    }

    // ─── GET /api/todos ─── Get all todos
    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAll()
    {
        var todos = await _db.Todos.ToListAsync();
        return Ok(todos);
    }

    // ─── GET /api/todos/5 ─── Get one todo by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var todo = await _db.Todos.FindAsync(id);
        if (todo == null) return NotFound();
        return Ok(todo);
    }

    // ─── POST /api/todos ─── Create a new todo
    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem item)
    {
        _db.Todos.Add(item);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    // ─── PUT /api/todos/5 ─── Update an existing todo
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItem item)
    {
        if (id != item.Id) return BadRequest();

        var existing = await _db.Todos.FindAsync(id);
        if (existing == null) return NotFound();

        existing.Title = item.Title;
        existing.IsCompleted = item.IsCompleted;
        await _db.SaveChangesAsync();

        return NoContent();
    }

    // ─── DELETE /api/todos/5 ─── Delete a todo
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var todo = await _db.Todos.FindAsync(id);
        if (todo == null) return NotFound();

        _db.Todos.Remove(todo);
        await _db.SaveChangesAsync();

        return NoContent();
    }
}