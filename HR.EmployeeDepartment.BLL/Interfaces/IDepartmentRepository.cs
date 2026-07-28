using HR.EmployeeDepartment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.EmployeeDepartment.BLL.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAll();
        Task<Department> Get(int id);
        Task<int> Add (Department department);
        Task<int> Update(Department department);
        Task<int> Delete(Department department);

    }
}
