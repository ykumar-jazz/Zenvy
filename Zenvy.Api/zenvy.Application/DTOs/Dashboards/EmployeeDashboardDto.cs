namespace zenvy.application.DTOs.Dashboards;

/// <summary>
/// Employee Dashboard - Task and performance tracking
/// </summary>
public class EmployeeDashboardKpiCards
{
    public int TasksAssigned { get; set; }
    public int TasksCompleted { get; set; }
    public int AttendanceDays { get; set; }
    public decimal Salary { get; set; }
    public decimal Commission { get; set; }
    public decimal PerformanceScore { get; set; }
}

public class EmployeeTaskInfo
{
    public int TaskId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime DueDate { get; set; }
}

public class SalesEmployeeView
{
    public decimal SalesToday { get; set; }
    public int OrdersProcessed { get; set; }
    public int ReturnsHandled { get; set; }
}

public class WarehouseEmployeeView
{
    public int StockReceived { get; set; }
    public int StockDispatched { get; set; }
    public int InventoryAdjustments { get; set; }
}

public class ProductionEmployeeView
{
    public int ProductionOrders { get; set; }
    public int CompletedUnits { get; set; }
    public decimal EfficiencyPercent { get; set; }
}

public class EmployeeDashboardResponse
{
    public EmployeeDashboardKpiCards KpiCards { get; set; } = new();
    public List<EmployeeTaskInfo> AssignedTasks { get; set; } = [];
    
    // Role-specific views
    public SalesEmployeeView? SalesView { get; set; }
    public WarehouseEmployeeView? WarehouseView { get; set; }
    public ProductionEmployeeView? ProductionView { get; set; }
}
