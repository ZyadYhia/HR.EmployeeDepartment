using HR.EmployeeDepartment.BLL.Interfaces;
using HR.EmployeeDepartment.DAL.Data.DbContexts;
using HR.EmployeeDepartment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.EmployeeDepartment.BLL.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {

        public EmployeeRepository(AppDbContext ctx): base(ctx)
        {
            
        }

    }
}
