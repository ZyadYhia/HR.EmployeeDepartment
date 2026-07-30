using System.Threading.Tasks;
using HR.EmployeeDepartment.BLL.Interfaces;
using HR.EmployeeDepartment.DAL.Models;
using HR.EmployeeDepartment.PL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HR.EmployeeDepartment.PL.Controllers;
public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }
                        
    public async Task<IActionResult> Index()
    {
        var model = await _employeeRepository.GetAll();
        return View(model);
    }

    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (id is null) return BadRequest(new { statusCode = 400, message = "Id is required" });
        var model = await _employeeRepository.Get(id.Value);
        if (model == null)
            return NotFound(new { statusCode = 404, message = $"Employee with Id:{id} not found" });
        return View(viewName, model);
    }

    [HttpGet]
    public IActionResult Create()
    {
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
                CreatedAt = model.CreatedAt
            };
            var res = await _employeeRepository.Add(employee);
            if (res > 0)
                return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id) => await Details(id, "Edit");

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
        return View(model);
    }
}
