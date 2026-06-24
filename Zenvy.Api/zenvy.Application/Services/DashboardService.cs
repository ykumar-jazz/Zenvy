using zenvy.application.DTOs.Dashboards;
using zenvy.application.Interfaces.Repositories;
using zenvy.application.Interfaces.Services;

namespace zenvy.Application.Services;

public class DashboardService(IDashboardRepository dashboardRepository) : IDashboardService
{
    public async Task<DashboardResponse<ExecutiveDashboardResponse>> GetExecutiveDashboardAsync()
    {
        try
        {
            var data = await dashboardRepository.GetExecutiveDashboardAsync();
            return new DashboardResponse<ExecutiveDashboardResponse>
            {
                Success = data != null,
                Message = data != null ? "Executive dashboard retrieved successfully" : "Failed to retrieve executive dashboard",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new DashboardResponse<ExecutiveDashboardResponse>
            {
                Success = false,
                Message = $"Error retrieving executive dashboard: {ex.Message}"
            };
        }
    }

    public async Task<DashboardResponse<FinanceDashboardResponse>> GetFinanceDashboardAsync()
    {
        try
        {
            var data = await dashboardRepository.GetFinanceDashboardAsync();
            return new DashboardResponse<FinanceDashboardResponse>
            {
                Success = data != null,
                Message = data != null ? "Finance dashboard retrieved successfully" : "Failed to retrieve finance dashboard",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new DashboardResponse<FinanceDashboardResponse>
            {
                Success = false,
                Message = $"Error retrieving finance dashboard: {ex.Message}"
            };
        }
    }

    public async Task<DashboardResponse<OperationsDashboardResponse>> GetOperationsDashboardAsync()
    {
        try
        {
            var data = await dashboardRepository.GetOperationsDashboardAsync();
            return new DashboardResponse<OperationsDashboardResponse>
            {
                Success = data != null,
                Message = data != null ? "Operations dashboard retrieved successfully" : "Failed to retrieve operations dashboard",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new DashboardResponse<OperationsDashboardResponse>
            {
                Success = false,
                Message = $"Error retrieving operations dashboard: {ex.Message}"
            };
        }
    }

    public async Task<DashboardResponse<MarketplaceDashboardResponse>> GetMarketplaceDashboardAsync()
    {
        try
        {
            var data = await dashboardRepository.GetMarketplaceDashboardAsync();
            return new DashboardResponse<MarketplaceDashboardResponse>
            {
                Success = data != null,
                Message = data != null ? "Marketplace dashboard retrieved successfully" : "Failed to retrieve marketplace dashboard",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new DashboardResponse<MarketplaceDashboardResponse>
            {
                Success = false,
                Message = $"Error retrieving marketplace dashboard: {ex.Message}"
            };
        }
    }

    public async Task<DashboardResponse<InvestorDashboardResponse>> GetInvestorDashboardAsync(int investorId)
    {
        try
        {
            var data = await dashboardRepository.GetInvestorDashboardAsync(investorId);
            return new DashboardResponse<InvestorDashboardResponse>
            {
                Success = data != null,
                Message = data != null ? "Investor dashboard retrieved successfully" : "Investor not found",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new DashboardResponse<InvestorDashboardResponse>
            {
                Success = false,
                Message = $"Error retrieving investor dashboard: {ex.Message}"
            };
        }
    }

    public async Task<DashboardResponse<EmployeeDashboardResponse>> GetEmployeeDashboardAsync(int? employeeId = null)
    {
        try
        {
            var data = await dashboardRepository.GetEmployeeDashboardAsync(employeeId);
            return new DashboardResponse<EmployeeDashboardResponse>
            {
                Success = data != null,
                Message = data != null ? "Employee dashboard retrieved successfully" : "Employee not found",
                Data = data
            };
        }
        catch (Exception ex)
        {
            return new DashboardResponse<EmployeeDashboardResponse>
            {
                Success = false,
                Message = $"Error retrieving employee dashboard: {ex.Message}"
            };
        }
    }
}
