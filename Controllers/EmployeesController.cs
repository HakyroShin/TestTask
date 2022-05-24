using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employee.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;


namespace Employee.Controllers;

public class EmployeesController : Controller
{
    private readonly ILogger<EmployeesController> _logger;
    CountTimeApplication _context;
    public EmployeesController(ILogger<EmployeesController> logger, CountTimeApplication context)
    {
        _logger = logger;
        _context = context;
    }
    public IActionResult Index()
    {
        return View(_context.Employees.ToList());
    }
    // создание
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(Employee.Models.Employee employee)
    {
        _context.Employees.Add(employee);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    // редактирование
    public async Task<IActionResult> Edit(int id)
    {
        
            Employee.Models.Employee employee = await _context.Employees.FindAsync(id);
            if (employee != null)
                return View(employee);
    
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Employee.Models.Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    // удаление
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
       
            Employee.Models.Employee employee = await _context.Employees.FindAsync(id);
            if ( _context.Times.Where(p => p.EmployeeId == id).Count() > 0)
            {
                return BadRequest("Сотрудник работает над задачей");
            }

            else
            {
                if (employee != null)
                {
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
                }
            }

        return NotFound();
    }
}
