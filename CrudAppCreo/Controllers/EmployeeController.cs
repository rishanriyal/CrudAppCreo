using CrudAppCreo.Models;
using CrudAppCreo.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CrudAppCreo.Controllers
{
    public class EmployeeController : Controller
    {
        IUnitOfWork _uow;

        public EmployeeController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public IActionResult Index()
        {
            IEnumerable<Employee> employees = _uow.EmployeeRepository.GetAll();
            return View(employees);
        }

        public IActionResult Delete(int id)
        {
            _uow.EmployeeRepository.DeleteEmployeeById(id);
            _uow.Save();

            return View();
        }

        //public IActionResult Edit(Employee employee)
        //{

        //}

    }
}
