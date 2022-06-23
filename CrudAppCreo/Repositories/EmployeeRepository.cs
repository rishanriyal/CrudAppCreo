using CrudAppCreo.Models;
using CrudAppCreo.Models.ViewModels;

namespace CrudAppCreo.Repositories
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        void DeleteEmployeeById(int employeeId);
        IEnumerable<EmployeeSalaryViewModel> GetEmployeeDetailsWithSalary();
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

        public IEnumerable<EmployeeSalaryViewModel> GetEmployeeDetailsWithSalary()
        {
            var objQuery = (from emp in _dbContext.Set<Employee>()
                            join sal in _dbContext.Set<Salary>() on emp.EmployeeId equals sal.EmployeeId
                            select new EmployeeSalaryViewModel
                            {
                                EmployeeId = emp.EmployeeId,
                                FirstName = emp.FirstName,
                                LastName = emp.LastName,
                                Designation = emp.Designation,
                                DateOfBirth = emp.DateOfBirth,
                                Mobile = emp.Mobile,
                                Email = emp.Email,
                                Address = emp.Address,
                                NetSalary = sal.NetSalary
                            }).ToList();

           return objQuery;
        }

    }
}
