﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrudAppCreo.Models;
using CrudAppCreo.Repositories;
using CrudAppCreo.Models.ViewModels;

namespace CrudAppCreo.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly CrudAppCreoDbContext _context;
        IUnitOfWork _uow;

        public EmployeesController(IUnitOfWork uow, CrudAppCreoDbContext context)
        {
            _context = context;
            _uow = uow;
        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            IEnumerable<EmployeeSalaryViewModel> employees = _uow.EmployeeRepository.GetEmployeeDetailsWithSalary();
            
            return employees != null ? View(employees) : Problem("Table set 'Employees' is null.");
        }

        // GET: Employees/Details/{id}
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _uow.EmployeeRepository.FindById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,FirstName,LastName,DateOfBirth,Mobile,Email,Address,Designation")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _uow.EmployeeRepository.Add(employee);
                await _uow.SaveAsync();
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<EmployeeSalaryViewModel> employees = _uow.EmployeeRepository.GetEmployeeDetailsWithSalary();
            return View(employees);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = _uow.EmployeeRepository.FindById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,FirstName,LastName,DateOfBirth,Mobile,Email,Address,Designation")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _uow.EmployeeRepository.Update(employee);
                    await _uow.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = _uow.EmployeeRepository.FindById(id.Value);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employees = _uow.EmployeeRepository.GetAll();
            if (employees == null)
            {
                return Problem("Entity set 'Employees'  is null.");
            }
            var employee = _uow.EmployeeRepository.FindById(id);
            if (employee != null)
            {
                _uow.EmployeeRepository.Remove(employee);
            }
            
            await _uow.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            var employee = _uow.EmployeeRepository.FindById(id);
            return employee == null ? false : true;
        }
    }
}
