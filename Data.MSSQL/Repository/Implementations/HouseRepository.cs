using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class HouseRepository : Repository<House>, IHouseRepository
{
    public HouseRepository(AppDbContext context) : base(context)
    {
    }
}