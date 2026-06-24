using zenvy.application.DTOs.Dashboards;

namespace zenvy.application.Interfaces.Repositories;

public interface IDashboardRepository
{
    Task<ExecutiveDashboardResponse?> GetExecutiveDashboardAsync();
    Task<FinanceDashboardResponse?> GetFinanceDashboardAsync();
    Task<OperationsDashboardResponse?> GetOperationsDashboardAsync();
    Task<MarketplaceDashboardResponse?> GetMarketplaceDashboardAsync();
    Task<InvestorDashboardResponse?> GetInvestorDashboardAsync(int investorId);
    Task<EmployeeDashboardResponse?> GetEmployeeDashboardAsync(int? employeeId = null);
}
