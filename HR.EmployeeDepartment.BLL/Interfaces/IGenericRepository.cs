using HR.EmployeeDepartment.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.EmployeeDepartment.BLL.Interfaces;
public interface IGenericRepository<T> where T : BaseModel
{
    Task<IEnumerable<T>> GetAll();
    Task<T> Get(int id);
    Task<int> Add(T model);
    Task<int> Update(T model);
    Task<int> Delete(T model);
}
