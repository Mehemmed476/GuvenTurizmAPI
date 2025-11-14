using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class HouseAdvantageRepository : Repository<HouseAdvantage>, IHouseAdvantageRepository
{
    public HouseAdvantageRepository(AppDbContext context) : base(context)
    {
    }
}