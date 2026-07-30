using System;
using System.ComponentModel.DataAnnotations;

namespace HR.EmployeeDepartment.PL.DTOs;

public class CreateEmployeeDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    public int? Age { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string Email { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    [Phone]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Salary is required")]
    public decimal Salary { get; set; }

    // bools don't usually need [Required] — unchecked checkbox binds to false.
    public bool IsActive { get; set; }

    // Controlled server-side; keep but not required in the form.
    public bool IsDeleted { get; set; }

    [Required(ErrorMessage = "Hiring date is required")]
    public DateTime HiringDate { get; set; }

    [Required(ErrorMessage = "Created date is required")]
    public DateTime CreatedAt { get; set; }
}
