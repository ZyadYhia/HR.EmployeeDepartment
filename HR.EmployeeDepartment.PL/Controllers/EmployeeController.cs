using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using HR.EmployeeDepartment.BLL.Interfaces;
using HR.EmployeeDepartment.DAL.Models;
using HR.EmployeeDepartment.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HR.EmployeeDepartment.PL.Controllers;
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;

    public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
    {
        _employeeRepository = employeeRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<IActionResult> Index()
    {
        // Get employees and departments, then attach the department to each employee so views can show department name.
        var employees = (await _employeeRepository.GetAll())?.ToList() ?? new List<Employee>();
        var departments = (await _departmentRepository.GetAll())?.ToList() ?? new List<Department>();

        var deptDict = departments.ToDictionary(d => d.Id, d => d);
        foreach (var e in employees)
        {
            if (e.Department == null && e.DepartmentId.HasValue && deptDict.TryGetValue(e.DepartmentId.Value, out var dept))
                e.Department = dept;
        }

        return View(employees);
    }

    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (id is null) return BadRequest(new { statusCode = 400, message = "Id is required" });

        var model = await _employeeRepository.Get(id.Value);
        if (model == null)
            return NotFound(new { statusCode = 404, message = $"Employee with Id:{id} not found" });

        if (model.Department == null && model.DepartmentId.HasValue)
            model.Department = await _departmentRepository.Get(model.DepartmentId.Value);

        return View(viewName, model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var departments = await _departmentRepository.GetAll();
        ViewData["departments"] = departments;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEmployeeDto model)
    {
        if (ModelState.IsValid)
        {
            var employee = new Employee()
            {
                Name = model.Name,
                Age = model.Age,
                Email = model.Email,
                Address = model.Address,
                Phone = model.Phone,
                Salary = model.Salary,
                IsActive = model.IsActive,
                IsDeleted = model.IsDeleted,
                HiringDate = model.HiringDate,
                DepartmentId = model.DepartmentId,
                CreatedAt = model.CreatedAt
            };
            var res = await _employeeRepository.Add(employee);
            if (res > 0)
                return RedirectToAction(nameof(Index));
        }

        // repopulate departments when returning to the view due to validation errors
        ViewData["departments"] = await _departmentRepository.GetAll();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var departments = await _departmentRepository.GetAll();
        ViewData["departments"] = departments;
        return await Details(id, "Edit");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Employee model)
    {
        if (ModelState.IsValid)
        {
            var res = await _employeeRepository.Update(model);
            if (res > 0)
                return RedirectToAction(nameof(Index));
        }

        // repopulate departments when returning to the view due to validation errors
        ViewData["departments"] = await _departmentRepository.GetAll();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id) => await Details(id, "Delete");

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Employee model)
    {
        if (ModelState.IsValid)
        {
            var res = await _employeeRepository.Delete(model);
            if (res > 0)
                return RedirectToAction(nameof(Index));
        }

        // If delete fails or modelstate invalid, re-fetch the full entity so the Delete view shows current data.
        return await Details(model.Id, "Delete");
    }
}
