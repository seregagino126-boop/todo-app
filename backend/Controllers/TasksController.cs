using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers;

//Контроллер для работы с задачами
//Отвечает на HTTP-запросы по пути /api/tasks.
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    // Объект AppDbContext, через него шлем работы в Postgre
    //_context общается с реальной базой по сети
    private readonly AppDbContext _context;
    
    //Конструктор ASP.NET сам передает сюда AppDbContext(DI)
    public TasksController(AppDbContext context)
    {
        _context = context;
    }
    //Get запрос, возвращаем список всех задач из БД

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _context.Tasks.ToListAsync();
        return Ok(tasks);
    }
    
    //Get {id}, одну задачу
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    //POST запрос. Создаем новую задачу. Данные приходят в теле запроса
    //JSON. Возвращаем созданную задачу и ссылку на нее.
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskItem task)
    {
        task.CreatedAt = DateTime.UtcNow;
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPut("{id}")]
    //в параметрах id и данные из тела запроса(JSON)
    public async Task<IActionResult> Update(int id, [FromBody] TaskItem updatedTask)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return NotFound();

        //EF Core помечает объект как измененный
        task.Title = updatedTask.Title;
        task.Description = updatedTask.Description;
        task.IsDone = updatedTask.IsDone;
        //EF Core генерирует UPDATE SQL для измененных полей
        await _context.SaveChangesAsync();

        return Ok(task);
    }
}