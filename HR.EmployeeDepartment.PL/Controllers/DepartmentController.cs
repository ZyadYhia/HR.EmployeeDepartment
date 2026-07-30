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

    public async Task<IActionResult> Details(int? id)
    {
        if(id is null ) return BadRequest(new {statusCode = 400, message = "Id is required"});
        var dpt = await _departmentReposatory.Get(id.Value);
        if(dpt == null)
            return NotFound(new {statusCode = 404, message = $"Department with Id:{id} not found"});
        return View(dpt);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateDepartmentDto model)
    {
        if(ModelState.IsValid)
        {
            var dpt = new Department()
            {
                Code = model.Code,
                Name = model.Name,
                CreatedAt = model.CreatedAt
            };
            var res = await _departmentReposatory.Add(dpt);
            if(res > 0)
                return RedirectToAction(nameof(Index));

        }
        return View();
    }

    
}
