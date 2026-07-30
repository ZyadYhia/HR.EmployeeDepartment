using System.ComponentModel.DataAnnotations;

namespace HR.EmployeeDepartment.PL.DTOs;

public class EditDepartmentDto
{
    [Required(ErrorMessage = "Code is required")]
    public string Code { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "CreatedAt is required")]
    public DateTime CreatedAt { get; set; }
}
