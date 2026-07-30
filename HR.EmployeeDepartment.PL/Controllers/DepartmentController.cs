using HR.EmployeeDepartment.BLL.Interfaces;
using HR.EmployeeDepartment.DAL.Models;
using HR.EmployeeDepartment.PL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HR.EmployeeDepartment.PL.Controllers;
public class DepartmentController : Controller
{
    private readonly IDepartmentRepository _departmentReposatory;

    public DepartmentController(IDepartmentRepository departmentReposatory)
    {
        _departmentReposatory = departmentReposatory;
    }

    public async Task<IActionResult> Index()
    {
        var departments = await _departmentReposatory.GetAll();
        return View(departments);
    }

    public async Task<IActionResult> Details(int? id, string viewName = "Details")
    {
        if (id is null) return BadRequest(new { statusCode = 400, message = "Id is required" });
        var dpt = await _departmentReposatory.Get(id.Value);
        if (dpt == null)
            return NotFound(new { statusCode = 404, message = $"Department with Id:{id} not found" });
        return View(viewName, dpt);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentDto model)
    {
        if (ModelState.IsValid)
        {
            var dpt = new Department()
            {
                Code = model.Code,
                Name = model.Name,
                CreatedAt = model.CreatedAt
            };
            var res = await _departmentReposatory.Add(dpt);
            if (res > 0)
                return RedirectToAction(nameof(Index));

        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id) => await Details(id, "Edit");

    [HttpPost]
    //[ValidateAntiForgeryToken] // This line is commented out, but it is recommended to use it for security against CSRF attacks. and only accept requests from the same site.
    public async Task<IActionResult> Edit(Department model)
    {
        if (ModelState.IsValid)
        {
            var res = await _departmentReposatory.Update(model);
            if (res > 0)
                return RedirectToAction(nameof(Index));

        }
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id) => await Details(id, "Delete");

    public async Task<IActionResult> Delete(Department model)
    {
        if (ModelState.IsValid)
        {
            var res = await _departmentReposatory.Delete(model);
            if (res > 0)
                return RedirectToAction(nameof(Index));

        }
        return View();
    }

}
