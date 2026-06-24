namespace zenvy.application.DTOs.Dashboards;

/// <summary>
/// Marketplace Dashboard - Meesho, Myntra, Amazon tracking
/// </summary>
public class MarketplaceDashboardKpiCards
{
    public int TotalOrders { get; set; }
    public int DeliveredOrders { get; set; }
    public int CancelledOrders { get; set; }
    public int ReturnedOrders { get; set; }
    public int SettlementPending { get; set; }
    public decimal SettlementReceived { get; set; }
}

public class ChannelAnalytics
{
    public string ChannelName { get; set; } = string.Empty;
    public decimal ChannelRevenue { get; set; }
    public decimal ChannelProfit { get; set; }
    public decimal CommissionPercent { get; set; }
    public decimal ReturnPercent { get; set; }
    public decimal PenaltyPercent { get; set; }
}

public class ChannelPerformance
{
    public string ChannelName { get; set; } = string.Empty;
    public int OrderCount { get; set; }
    public int DeliveredCount { get; set; }
    public int ReturnCount { get; set; }
    public int CancelledCount { get; set; }
}

public class MarketplaceDashboardResponse
{
    public MarketplaceDashboardKpiCards KpiCards { get; set; } = new();
    public List<ChannelAnalytics> ChannelAnalytics { get; set; } = [];
    public List<ChannelPerformance> ChannelPerformance { get; set; } = [];
}
