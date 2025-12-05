using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class SettingRepository : Repository<Setting>, ISettingRepository
{
    public SettingRepository(AppDbContext context) : base(context)
    {
    }
}