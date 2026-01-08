using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class TourPackageInclusionRepository : Repository<TourPackageInclusion>, ITourPackageInclusionRepository
{
    public TourPackageInclusionRepository(AppDbContext context) : base(context)
    {
    }
}