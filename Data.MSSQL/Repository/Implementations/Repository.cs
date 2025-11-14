using System.Linq.Expressions;
using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Data.MSSQL.Repository.Implementations;

public class Repository<T> : IRepository<T> where T : BaseEntity, new()
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public async Task<ICollection<T>> GetAllAsync(params string[] includes)
    {
        IQueryable<T> query = Table.AsQueryable();

        if (includes.Length > 0)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }

        return await query.ToListAsync();
    }

    public IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> expression, params string[] includes)
    {
        IQueryable<T> query = Table.AsQueryable();
        
        if (includes.Length > 0)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        }
        
        return query.Where(expression);
    }

    public async Task<T> GetByIdAsync(Guid id, params string[] includes)
    {
        IQueryable<T> query = Table.AsQueryable();

        if (includes.Length > 0)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        } 

        T? entity = await query.FirstOrDefaultAsync(x => x.Id == id);

        return entity;
    }

    public async Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression, params string[] includes)
    {
        IQueryable<T> query = Table.AsQueryable();

        T? entity = await query.SingleOrDefaultAsync(expression);
        
        if (includes.Length > 0)
        {
            foreach (string include in includes)
            {
                query = query.Include(include);
            }
        } 
        
        return entity;
    }

    public async Task<int> SaveChangesAsync()
    {
        int rows = await _context.SaveChangesAsync();
        return rows;
    }

    public void Update(T entity)
    {
        Table.Update(entity);
    }
}