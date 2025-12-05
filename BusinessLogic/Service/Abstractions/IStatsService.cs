using BusinessLogic.DTO.StatsDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface IStatsService
{
    Task<DashboardStatsDTO> GetDashboardStatsAsync();
}