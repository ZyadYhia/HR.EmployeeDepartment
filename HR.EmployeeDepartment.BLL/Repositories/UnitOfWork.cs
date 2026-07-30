using HR.EmployeeDepartment.BLL.Interfaces;
using HR.EmployeeDepartment.DAL.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.EmployeeDepartment.BLL.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _ctx;

    public IDepartmentRepository DepartmentRepository { get; }

    public IEmployeeRepository EmployeeRepository { get; }
    public UnitOfWork(AppDbContext ctx)
    {
        _ctx = ctx;
        DepartmentRepository = new DepartmentRepository(_ctx);
        EmployeeRepository = new EmployeeRepository(_ctx);
        
    }

    public async Task<int> Complete() => await _ctx.SaveChangesAsync();
    // Implement IDisposable to dispose the context when the UnitOfWork is disposed
    // This is important to free up resources and avoid memory leaks.
    public void Dispose() => _ctx.Dispose();
}
