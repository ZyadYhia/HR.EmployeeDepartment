using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HR.EmployeeDepartment.PL.DTOs;

public class CreateEmployeeDto
{
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }

    public int? Age { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Address is required")]
    public string Address { get; set; }

    [Required(ErrorMessage = "Phone is required")]
    [Phone(ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; }

    [Required(ErrorMessage = "Salary is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Salary must be non-negative")]
    public decimal Salary { get; set; }

    // Default to true for new employees; can be changed when editing.
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = true;

    // Hiring date required for creation
    [Required(ErrorMessage = "Hiring date is required")]
    [DataType(DataType.Date)]
    public DateTime HiringDate { get; set; }
    [Required(ErrorMessage = "CreatedAt date is required")]
    [DataType(DataType.Date)]
    public DateTime CreatedAt { get; set; }

    // Matches Employee.DepartmentId (nullable)
    [DisplayName("Department")]
    public int? DepartmentId { get; set; }
}
