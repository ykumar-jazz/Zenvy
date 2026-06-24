namespace zenvy.application.DTOs.Dashboards;

/// <summary>
/// Finance Dashboard - Money management and fund flow
/// </summary>
public class FinanceDashboardKpiCards
{
    public decimal CashAvailable { get; set; }
    public decimal BankBalance { get; set; }
    public decimal WorkingCapital { get; set; }
    public decimal ReserveFund { get; set; }
    public decimal InvestorCapital { get; set; }
    public decimal OutstandingLoans { get; set; }
    public decimal Receivables { get; set; }
    public decimal Payables { get; set; }
}

public class FinanceDashboardReports
{
    public ProfitLossReport ProfitLoss { get; set; } = new();
    public CashFlowReport CashFlow { get; set; } = new();
    public FundFlowReport FundFlow { get; set; } = new();
}

public class ProfitLossReport
{
    public decimal Revenue { get; set; }
    public decimal Cogs { get; set; }
    public decimal GrossProfit { get; set; }
    public decimal OperatingExpenses { get; set; }
    public decimal NetProfit { get; set; }
    public decimal ProfitMarginPercent { get; set; }
}

public class CashFlowReport
{
    public decimal CashIn { get; set; }
    public decimal CashOut { get; set; }
    public decimal NetCashFlow { get; set; }
    public decimal FundUtilization { get; set; }
}

public class FundFlowReport
{
    public decimal OwnerInvestment { get; set; }
    public decimal InvestorInvestment { get; set; }
    public decimal LoanReceived { get; set; }
    public decimal SalesIncome { get; set; }
    public decimal TotalInflow { get; set; }
    public decimal PurchaseExpense { get; set; }
    public decimal OperatingExpense { get; set; }
    public decimal Salaries { get; set; }
    public decimal LoanEmi { get; set; }
    public decimal InvestorPayout { get; set; }
    public decimal TotalOutflow { get; set; }
}

public class FinanceDashboardResponse
{
    public FinanceDashboardKpiCards KpiCards { get; set; } = new();
    public FinanceDashboardReports Reports { get; set; } = new();
    public List<TrendDataPoint> CashTrend { get; set; } = [];
}
