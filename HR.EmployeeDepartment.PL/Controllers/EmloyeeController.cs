using HR.EmployeeDepartment.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HR.EmployeeDepartment.PL.Controllers;
public class EmloyeeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
