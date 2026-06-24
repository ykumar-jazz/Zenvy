using zenvy.application.DTOs.Dashboards;

namespace zenvy.application.Interfaces.Services;

public interface IDashboardService
{
    Task<DashboardResponse<ExecutiveDashboardResponse>> GetExecutiveDashboardAsync();
    Task<DashboardResponse<FinanceDashboardResponse>> GetFinanceDashboardAsync();
    Task<DashboardResponse<OperationsDashboardResponse>> GetOperationsDashboardAsync();
    Task<DashboardResponse<MarketplaceDashboardResponse>> GetMarketplaceDashboardAsync();
    Task<DashboardResponse<InvestorDashboardResponse>> GetInvestorDashboardAsync(int investorId);
    Task<DashboardResponse<EmployeeDashboardResponse>> GetEmployeeDashboardAsync(int? employeeId = null);
}
