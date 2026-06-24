namespace zenvy.application.DTOs.Dashboards;

/// <summary>
/// Operations Dashboard - Daily business operations
/// </summary>
public class OperationsDashboardKpiCards
{
    public int PendingPurchases { get; set; }
    public int PendingGoodsReceipt { get; set; }
    public decimal CurrentInventory { get; set; }
    public int PendingOrders { get; set; }
    public int PendingDispatch { get; set; }
    public int Returns { get; set; }
    public decimal DamagedStock { get; set; }
}

public class LowStockAlert
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int CurrentStock { get; set; }
    public int MinimumThreshold { get; set; }
}

public class TopSellingProduct
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int QuantitySold { get; set; }
    public decimal Revenue { get; set; }
}

public class PendingSupplierPayment
{
    public int SupplierId { get; set; }
    public string SupplierName { get; set; } = string.Empty;
    public decimal PendingAmount { get; set; }
    public DateTime DueDate { get; set; }
}

public class OperationsDashboardResponse
{
    public OperationsDashboardKpiCards KpiCards { get; set; } = new();
    public List<LowStockAlert> LowStockProducts { get; set; } = [];
    public List<TopSellingProduct> TopSellingProducts { get; set; } = [];
    public List<PendingSupplierPayment> PendingSupplierPayments { get; set; } = [];
}
