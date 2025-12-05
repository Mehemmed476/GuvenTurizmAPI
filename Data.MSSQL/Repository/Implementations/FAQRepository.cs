using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class FAQRepository : Repository<FAQ>, IFAQRepository
{
    public FAQRepository(AppDbContext context) : base(context)
    {
    }
}