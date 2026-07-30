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
    Task Add(T model);
    Task Update(T model);
    Task Delete(T model);
}
