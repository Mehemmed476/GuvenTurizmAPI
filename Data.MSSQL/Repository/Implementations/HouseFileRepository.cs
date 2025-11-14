using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class HouseFileRepository : Repository<HouseFile>, IHouseFileRepository
{
    public HouseFileRepository(AppDbContext context) : base(context)
    {
    }
}