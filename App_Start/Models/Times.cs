using System.ComponentModel.DataAnnotations.Schema;

namespace Employee.Models
{
    public class Time
    {
        public int Id {get; set;}
        public int EmployeeId {get; set;}
        [NotMapped]
        public Employee Employee {get; set;}
        public int TaskId {get; set;}
        [NotMapped]
        public Task Task {get; set;}
        public DateTime Date {get; set;}
        public int Minutes {get; set;}
    }
}