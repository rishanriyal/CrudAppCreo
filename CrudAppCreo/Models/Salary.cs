using System.ComponentModel.DataAnnotations;

namespace CrudAppCreo.Models
{
    public class Salary
    {
        [Key]
        public int SalaryId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Bonus { get; set; }
        public decimal Tax { get; set; }
        public decimal NetSalary { get; set; }

        public Employee Employee { get; set; } //Reference Navigation
        public int EmployeeId { get; set; } //Foreign key


    }
}
