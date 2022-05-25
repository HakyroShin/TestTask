using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Employee.Models;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Employee.Controllers;

public class Report
{
    public Employee.Models.Employee Employee { get; set; }
    public Employee.Models.Task Task { get; set; }
    public int Minutes { get; set; }
}

public class TimesController : Controller
{
    private readonly ILogger<TimesController> _logger;

    CountTimeApplication _context;

    public TimesController(ILogger<TimesController> logger, CountTimeApplication context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        List<Time> result = _context.Times.ToList();
        foreach (Time item in result)
        {
            item.Employee = _context.Employees.Find(item.EmployeeId);
            item.Task = _context.Tasks.Find(item.TaskId);
        }
        return View(result);
    }
    // создание
    public IActionResult Create()
    {
        ViewBag.Employees = new SelectList(_context.Employees.ToList(), "Id", "Name");
        ViewBag.Tasks = new SelectList(_context.Tasks.ToList(), "Id", "Name");
        return View();
    }
    [HttpPost]
    public IActionResult Create(Time time)
    {
        _context.Times.Add(time);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }
    // редактирование
    public async Task<IActionResult> Edit(int id)
    {
        ViewBag.Employees = new SelectList(_context.Employees.ToList(), "Id", "Name");
        ViewBag.Tasks = new SelectList(_context.Tasks.ToList(), "Id", "Name");
        Time time = await _context.Times.FindAsync(id);
        if (time != null)
            return View(time);
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(Time time)
    {
        _context.Times.Update(time);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    // удаление
    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {

        Time time = await _context.Times.FindAsync(id);
        if (time != null)
        {
            _context.Times.Remove(time);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return NotFound();
    }
    public IActionResult Report()
    {
        return View(new List<Report>());
    }
    [HttpPost]
    public IActionResult Report(int year, int month)
    {
        DateTime startday = new DateTime(year, month, 1);
        DateTime endday = startday.AddMonths(1).AddDays(-1);
        List<Report> result = new List<Report>();
        foreach (Employee.Models.Task t in _context.Tasks.ToList())
        {
            Report report = new Report();
            report.Task = t;

            foreach (Employee.Models.Employee e in _context.Employees.ToList())
            {
                int sum = 0;
                report.Minutes = 0;
                
                foreach (Employee.Models.Time ta in _context.Times.Where(p => p.TaskId == t.Id && p.EmployeeId == e.Id && p.Date >= startday && p.Date <= endday))
                {
                    sum += ta.Minutes;

                }
                if (report.Minutes < sum)
                {
                    report.Minutes = sum;
                    report.Employee = e;
                }

            }

            result.Add(report);
        }
        return View(result);
    }

}
