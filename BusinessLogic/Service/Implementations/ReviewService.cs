using BusinessLogic.DTO.ReviewDTOs;
using BusinessLogic.Service.Abstractions;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Service.Implementations;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _repo;

    public ReviewService(IReviewRepository repo)
    {
        _repo = repo;
    }

    public async Task CreateAsync(ReviewPostDTO dto, string userId, string userName)
    {
        if (dto.Rating < 1 || dto.Rating > 5) throw new ArgumentException("Reytinq 1-5 arası olmalıdır.");

        var review = new Review
        {
            HouseId = dto.HouseId,
            Text = dto.Text,
            Rating = dto.Rating,
            UserId = userId,
            UserName = userName
        };

        await _repo.AddAsync(review);
        await _repo.SaveChangesAsync();
    }

    public async Task<ICollection<ReviewGetDTO>> GetByHouseIdAsync(Guid houseId)
    {
        var reviews = await _repo
            .GetAllByCondition(r => !r.IsDeleted && r.HouseId == houseId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();

        return reviews.Select(r => new ReviewGetDTO
        {
            Id = r.Id,
            UserName = r.UserName,
            Text = r.Text,
            Rating = r.Rating,
            CreatedAt = r.CreatedAt
        }).ToList();
    }
}