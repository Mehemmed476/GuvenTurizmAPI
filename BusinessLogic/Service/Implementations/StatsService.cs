using System.Globalization;
using BusinessLogic.DTO.StatsDTOs;
using BusinessLogic.Service.Abstractions;
using Data.MSSQL.Repository.Abstractions;
using Domain.Enums;

namespace BusinessLogic.Service.Implementations;

public class StatsService : IStatsService
{
    private readonly IBookingRepository _bookingRepo;
    private readonly IHouseRepository _houseRepo;

    public StatsService(IBookingRepository bookingRepo, IHouseRepository houseRepo)
    {
        _bookingRepo = bookingRepo;
        _houseRepo = houseRepo;
    }

    public async Task<DashboardStatsDTO> GetDashboardStatsAsync()
    {
        // 1. Bütün məlumatları çək
        var bookings = await _bookingRepo.GetAllAsync("House");
        var houses = _houseRepo.GetAllByCondition(h => !h.IsDeleted);

        // 2. Təsdiqlənmiş sifarişlər (Gəlir hesablamaq üçün)
        var confirmedBookings = bookings
            .Where(b => b.Status == BookingStatus.Confirmed && !b.IsDeleted)
            .ToList();

        // 3. Ümumi Gəlir Hesablanması (Gün sayı * Qiymət)
        decimal totalRevenue = confirmedBookings.Sum(b => {
            if (b.House == null) return 0;
            var days = (b.EndDate - b.StartDate).Days;
            return days > 0 ? days * b.House.Price : 0;
        });

        // 4. Aylıq Statistika (Son 6 ay) - Qrafik üçün
        var monthlyStats = new List<MonthlyStatDTO>();
        var culture = new CultureInfo("az-Latn-AZ"); // Azərbaycan ayları

        for (int i = 5; i >= 0; i--)
        {
            var date = DateTime.Now.AddMonths(-i);
            var monthName = date.ToString("MMM", culture);
            
            var monthBookings = confirmedBookings
                .Where(b => b.StartDate.Month == date.Month && b.StartDate.Year == date.Year)
                .ToList();

            monthlyStats.Add(new MonthlyStatDTO
            {
                Month = monthName,
                Revenue = monthBookings.Sum(b => {
                    var days = (b.EndDate - b.StartDate).Days;
                    return days > 0 ? days * (b.House?.Price ?? 0) : 0;
                }),
                Count = monthBookings.Count
            });
        }

        return new DashboardStatsDTO
        {
            TotalRevenue = totalRevenue,
            TotalBookings = bookings.Count,
            PendingBookings = bookings.Count(b => b.Status == BookingStatus.Pending && !b.IsDeleted),
            TotalHouses = houses.Count(),
            MonthlyStats = monthlyStats
        };
    }
}