using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using HR.EmployeeDepartment.BLL.Interfaces;
using HR.EmployeeDepartment.DAL.Models;
using HR.EmployeeDepartment.PL.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace HR.EmployeeDepartment.PL.Controllers;
public class EmployeeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public EmployeeController(IUnitOfWork unitOfWork, IMapper mapper )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        // Get employees and departments, then attach the department to each employee so views can show department name.
        var employees = (await _unitOfWork.EmployeeRepository.GetAll())?.ToList() ?? new List<Employee>();
        var departments = (await _unitOfWork.DepartmentRepository.GetAll())?.ToList() ?? new List<Department>();

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

        var model = await _unitOfWork.EmployeeRepository.Get(id.Value);
        if (model == null)
            return NotFound(new { statusCode = 404, message = $"Employee with Id:{id} not found" });

        if (model.Department == null && model.DepartmentId.HasValue)
            model.Department = await _unitOfWork.DepartmentRepository.Get(model.DepartmentId.Value);

        return View(viewName, model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var departments = await _unitOfWork.DepartmentRepository.GetAll();
        ViewData["departments"] = departments;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateEmployeeDto model)
    {
        if (ModelState.IsValid)
        {
            var employee = _mapper.Map<Employee>(model);
            //var employee = new Employee()
            //{
            //    Name = model.Name,
            //    Age = model.Age,
            //    Email = model.Email,
            //    Address = model.Address,
            //    Phone = model.Phone,
            //    Salary = model.Salary,
            //    IsActive = model.IsActive,
            //    IsDeleted = model.IsDeleted,
            //    HiringDate = model.HiringDate,
            //    DepartmentId = model.DepartmentId,
            //    CreatedAt = model.CreatedAt
            //};
            await _unitOfWork.EmployeeRepository.Add(employee);
            var res = await _unitOfWork.Complete();
            if (res > 0)
                return RedirectToAction(nameof(Index));
        }

        // repopulate departments when returning to the view due to validation errors
        ViewData["departments"] = await _unitOfWork.DepartmentRepository.GetAll();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        var departments = await _unitOfWork.DepartmentRepository.GetAll();
        ViewData["departments"] = departments;
        return await Details(id, "Edit");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Employee model)
    {
        if (ModelState.IsValid)
        {
            await _unitOfWork.EmployeeRepository.Update(model);
            var res = await _unitOfWork.Complete();
            if (res > 0)
                return RedirectToAction(nameof(Index));
        }

        // repopulate departments when returning to the view due to validation errors
        ViewData["departments"] = await _unitOfWork.DepartmentRepository.GetAll();
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
            await _unitOfWork.EmployeeRepository.Delete(model);
            var res = await _unitOfWork.Complete();
            if (res > 0)
                return RedirectToAction(nameof(Index));
        }

        // If delete fails or modelstate invalid, re-fetch the full entity so the Delete view shows current data.
        return await Details(model.Id, "Delete");
    }
}
