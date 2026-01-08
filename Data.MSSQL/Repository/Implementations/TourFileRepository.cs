using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class TourFileRepository : Repository<TourFile>, ITourFileRepository
{
    public TourFileRepository(AppDbContext context) : base(context)
    {
    }
}