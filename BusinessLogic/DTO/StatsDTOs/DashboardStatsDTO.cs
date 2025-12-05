namespace BusinessLogic.DTO.StatsDTOs;

public class DashboardStatsDTO
{
    public decimal TotalRevenue { get; set; } // Ümumi Gəlir
    public int TotalBookings { get; set; }    // Cəmi Sifariş
    public int PendingBookings { get; set; }  // Gözləyən
    public int TotalHouses { get; set; }      // Aktiv Evlər
    public List<MonthlyStatDTO> MonthlyStats { get; set; } = new(); // Qrafik üçün
}