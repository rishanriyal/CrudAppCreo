using System.ComponentModel.DataAnnotations;

namespace CrudAppCreo.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public Salary Salary { get; set; } //Reference navigation

    }
}
