namespace zenvy.application.DTOs.Dashboards;

/// <summary>
/// Investor Dashboard - Investment and profit tracking
/// </summary>
public class InvestorDashboardKpiCards
{
    public decimal TotalInvested { get; set; }
    public decimal CurrentCapital { get; set; }
    public decimal ProfitEarned { get; set; }
    public decimal ProfitPaid { get; set; }
    public decimal PendingProfit { get; set; }
    public decimal RoiPercent { get; set; }
}

public class InvestmentStatement
{
    public string InvestmentType { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime InvestmentDate { get; set; }
}

public class ProfitStatement
{
    public string Period { get; set; } = string.Empty;
    public decimal ProfitEarned { get; set; }
    public decimal ProfitPaid { get; set; }
    public decimal PendingProfit { get; set; }
}

public class WithdrawalStatement
{
    public decimal WithdrawalAmount { get; set; }
    public DateTime WithdrawalDate { get; set; }
    public string WithdrawalMode { get; set; } = string.Empty;
}

public class InvestorDashboardResponse
{
    public InvestorDashboardKpiCards KpiCards { get; set; } = new();
    public List<InvestmentStatement> InvestmentHistory { get; set; } = [];
    public List<ProfitStatement> ProfitStatements { get; set; } = [];
    public List<WithdrawalStatement> WithdrawalHistory { get; set; } = [];
    public List<TrendDataPoint> InvestmentGrowth { get; set; } = [];
    public List<TrendDataPoint> MonthlyRoi { get; set; } = [];
}
