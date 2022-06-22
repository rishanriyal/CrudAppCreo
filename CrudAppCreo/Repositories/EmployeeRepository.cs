using CrudAppCreo.Models;

namespace CrudAppCreo.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public void DeleteEmployeeById(int employeeId);
    }
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        CrudAppCreoDbContext _dbContext;

        public EmployeeRepository(CrudAppCreoDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteEmployeeById(int employeeId)
        {
            Employee employee = _dbContext.Employees.FirstOrDefault(x => x.EmployeeId == employeeId);

            if (employee != null)
            {
                _dbContext.Remove(employee);
            }
        }

    }
}
