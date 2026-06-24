namespace zenvy.application.DTOs.Dashboards;

/// <summary>
/// Executive/Admin Dashboard - Business health overview
/// </summary>
public class ExecutiveDashboardKpiCards
{
    public decimal TodaysSales { get; set; }
    public decimal MonthlySales { get; set; }
    public decimal MonthlyProfit { get; set; }
    public decimal InventoryValue { get; set; }
    public decimal AvailableCash { get; set; }
    public decimal ReserveFund { get; set; }
    public decimal InvestorLiability { get; set; }
    public int PendingOrders { get; set; }
    public int PendingReturns { get; set; }
}

public class ExecutiveDashboardCharts
{
    public List<TrendDataPoint> SalesTrend { get; set; } = [];
    public List<TrendDataPoint> ProfitTrend { get; set; } = [];
    public List<TrendDataPoint> CashTrend { get; set; } = [];
    public List<TrendDataPoint> InventoryTrend { get; set; } = [];
}

public class ExecutiveDashboardResponse
{
    public ExecutiveDashboardKpiCards KpiCards { get; set; } = new();
    public ExecutiveDashboardCharts Charts { get; set; } = new();
}
