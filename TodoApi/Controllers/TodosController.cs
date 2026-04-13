using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodosController : ControllerBase
{
    private readonly ITodoService _service;

    public TodosController(ITodoService service)
    {
        _service = service;
    }

    // GET /api/todos
    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAll()
    {
        var todos = await _service.GetAllAsync();
        return Ok(todos);
    }

    // GET /api/todos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    // POST /api/todos
    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem item)
    {
        var created = await _service.CreateAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT /api/todos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItem item)
    {
        var success = await _service.UpdateAsync(id, item);
        if (!success) return NotFound();
        return NoContent();
    }

    // DELETE /api/todos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
