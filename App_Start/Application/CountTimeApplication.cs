using Microsoft.EntityFrameworkCore;


namespace Employee.Models;

public class CountTimeApplication : DbContext
{
    public DbSet<Employee> Employees {get;set;}= null!;
    public DbSet<Task> Tasks {get; set; }= null!;
    public DbSet<Time> Times {get; set; }= null!; 
    public CountTimeApplication (DbContextOptions<CountTimeApplication> options)
    : base(options)
    {
        
    }

}