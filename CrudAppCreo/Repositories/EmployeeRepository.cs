using CrudAppCreo.Models;

namespace CrudAppCreo.Repositories
{
    public interface IEmployeeRepository
    {
        public IEnumerable<Employee> GetAllEmployees();
        public void DeleteEmployee(int employeeId);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        CrudAppCreoDbContext _dbContext;

        public EmployeeRepository(CrudAppCreoDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteEmployee(int employeeId)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);

            if (employee != null)
            {
                _dbContext.Remove(employee);
            }
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _dbContext.Employees.Select(s => s).ToList();
        }
    }
}
