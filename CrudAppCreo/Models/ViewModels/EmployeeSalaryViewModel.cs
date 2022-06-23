namespace CrudAppCreo.Models.ViewModels
{
    public class EmployeeSalaryViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Designation { get; set; }
        public decimal NetSalary { get; set; }

    }
}
