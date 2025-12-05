using BusinessLogic.DTO.ReviewDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface IReviewService
{
    Task CreateAsync(ReviewPostDTO dto, string userId, string userName);
    Task<ICollection<ReviewGetDTO>> GetByHouseIdAsync(Guid houseId);
}