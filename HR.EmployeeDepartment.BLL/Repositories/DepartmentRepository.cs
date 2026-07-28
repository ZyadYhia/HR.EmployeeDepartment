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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _ctx;

        public DepartmentRepository(AppDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IEnumerable<Department>> GetAll() => await _ctx.Departments.ToListAsync();
        public async Task<Department> Get(int id) => await _ctx.Departments.FirstOrDefaultAsync(d => d.Id == id);

        public async Task<int> Add(Department department) {
            await _ctx.Departments.AddAsync(department);
            return await _ctx.SaveChangesAsync();
        }
        public async Task<int> Update(Department department)
        {
            _ctx.Departments.Update(department);
            return await  _ctx.SaveChangesAsync();
        }
        public async Task<int> Delete(Department department)
        {
            _ctx.Departments.Remove(department);
            return await _ctx.SaveChangesAsync();
        }
    }
}
