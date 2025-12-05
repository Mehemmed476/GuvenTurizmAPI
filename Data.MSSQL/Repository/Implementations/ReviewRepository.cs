using Data.MSSQL.Context;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;

namespace Data.MSSQL.Repository.Implementations;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(AppDbContext context) : base(context)
    {
    }
}