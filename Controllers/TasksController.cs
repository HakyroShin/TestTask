using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employee.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;

namespace Employee.Controllers;

public class TasksController : Controller
{
    private readonly ILogger<TasksController> _logger;

    CountTimeApplication _context;

    public TasksController(ILogger<TasksController> logger, CountTimeApplication context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View(_context.Tasks.ToList());
    }
    // создание
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Employee.Models.Task task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    // редактирование
    public async Task<IActionResult> Edit(int id)
    {

        Employee.Models.Task task = await _context.Tasks.FindAsync(id);
        if (task != null)
            return View(task);

        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Employee.Models.Task task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    // удаление
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        Employee.Models.Task task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return NotFound();
    }

}
