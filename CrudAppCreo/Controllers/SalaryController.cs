using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudAppCreo.Models;
using CrudAppCreo.Repositories;

namespace CrudAppCreo.Controllers
{
    public class SalaryController : Controller
    {
        IUnitOfWork _uow;

        public SalaryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: All Salaries
        public async Task<IActionResult> Index()
        {
            var salaries = _uow.SalaryRepository.GetAll();
            return salaries != null ? View(salaries) : Problem("Table set 'Salaries' is null.");
        }

        // GET: Salary/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            Salary salary = new Salary();

            if (id == null)
            {
                return NotFound();
            }
            else
            {
                salary = _uow.SalaryRepository.FindById(id.Value);
            }
           
            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // GET: Salary/Create
        public IActionResult Create()
        {
            var employees = _uow.EmployeeRepository.GetAll();
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Salary/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalaryId,BasicSalary,Bonus,Tax,NetSalary,EmployeeId")] Salary salary)
        {
            if (ModelState.IsValid)
            {
                _uow.SalaryRepository.Add(salary);
                _uow.Save();
                return RedirectToAction(nameof(Index));
            }

            var employees = _uow.EmployeeRepository.GetAll();
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "EmployeeId", salary.EmployeeId);

            return View(salary);
        }

        // GET: Salary/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = _uow.SalaryRepository.FindById(id.Value);
            if (salary == null)
            {
                return NotFound();
            }

            var employees = _uow.EmployeeRepository.GetAll();

            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "EmployeeId", salary.EmployeeId);
            return View(salary);
        }

        // POST: Salary/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalaryId,BasicSalary,Bonus,Tax,NetSalary,EmployeeId")] Salary salary)
        {
            if (id != salary.SalaryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.SalaryRepository.Update(salary);
                    _uow.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalaryExists(salary.SalaryId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            var employees = _uow.EmployeeRepository.GetAll();
            
            ViewData["EmployeeId"] = new SelectList(employees, "EmployeeId", "EmployeeId", salary.EmployeeId);
            return View(salary);
        }

        // GET: Salary/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salary = _uow.SalaryRepository.FindById(id.Value);

            if (salary == null)
            {
                return NotFound();
            }

            return View(salary);
        }

        // POST: Salary/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salary = _uow.SalaryRepository.FindById(id);
            if (salary != null)
            {
                _uow.SalaryRepository.Remove(salary);
            }

            _uow.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool SalaryExists(int id)
        {
            var salary = _uow.SalaryRepository.FindById(id);
            return salary == null ? false : true;
        }
    }
}
