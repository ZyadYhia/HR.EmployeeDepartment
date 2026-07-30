using HR.EmployeeDepartment.BLL.Interfaces;
using HR.EmployeeDepartment.DAL.Data.DbContexts;
using HR.EmployeeDepartment.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.EmployeeDepartment.BLL.Repositories;
public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
{
    private readonly AppDbContext _ctx;

    public GenericRepository(AppDbContext ctx)
    {
        _ctx = ctx;
    }
    public async Task<IEnumerable<T>> GetAll() => await _ctx.Set<T>().ToListAsync<T>();
    public async Task<T> Get(int id) => await _ctx.Set<T>().FirstOrDefaultAsync<T>(d => d.Id == id);

    public async Task<int> Add(T model)
    {
        await _ctx.Set<T>().AddAsync(model);
        return await _ctx.SaveChangesAsync();
    }
    public async Task<int> Update(T model)
    {
        _ctx.Set<T>().Update(model);
        return await _ctx.SaveChangesAsync();
    }
    public async Task<int> Delete(T model)
    {
        _ctx.Set<T>().Remove(model);
        return await _ctx.SaveChangesAsync();
    }
}
