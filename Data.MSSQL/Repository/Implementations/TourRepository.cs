using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class TourRepository : Repository<Tour>, ITourRepository
{
    public TourRepository(AppDbContext context) : base(context)
    {
    }
}