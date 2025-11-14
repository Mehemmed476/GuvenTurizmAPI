using System.Linq.Expressions;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Data.MSSQL.Repository.Abstractions;

public interface IRepository<T> where T : BaseEntity, new()
{
    DbSet<T> Table { get; }

    Task<ICollection<T>> GetAllAsync(params string[] includes);
    IQueryable<T> GetAllByCondition(Expression<Func<T, bool>> expression,  params string[] includes);
    Task<T> GetByIdAsync(Guid id, params string[] includes);
    Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression, params string[] includes);

    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<int> SaveChangesAsync();
}