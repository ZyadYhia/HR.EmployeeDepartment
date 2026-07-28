using HR.EmployeeDepartment.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HR.EmployeeDepartment.PL.Controllers;
public class DepartmentController : Controller
{
    private readonly IDepartmentRepository _departmentReposatory;

    public DepartmentController(IDepartmentRepository departmentReposatory)
    {
        _departmentReposatory = departmentReposatory;
    }
    public IActionResult Index()
    {
        var deps = _departmentReposatory.GetAll();
        return View();
    }
}
