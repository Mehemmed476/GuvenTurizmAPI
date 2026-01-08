using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class TourPackageRepository : Repository<TourPackage>, ITourPackageRepository
{
    public TourPackageRepository(AppDbContext context) : base(context)
    {
    }
}