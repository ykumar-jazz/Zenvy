namespace zenvy.application.DTOs.MarketplaceSettlements;

public class MarketplaceSettlementRequest
{
    public long OrderId { get; set; }
    public int ChannelId { get; set; }
    public decimal SaleAmount { get; set; }
    public decimal MarketplaceCommission { get; set; }
    public decimal ShippingCharge { get; set; }
    public decimal TaxDeduction { get; set; }
    public string Status { get; set; } = "PENDING";
    public DateTime? SettlementDate { get; set; }
}

public class MarketplaceSettlementResponse
{
    public long SettlementId { get; set; }
    public long OrderId { get; set; }
    public string? ExternalOrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal OrderGrandTotal { get; set; }
    public int ChannelId { get; set; }
    public string ChannelName { get; set; } = string.Empty;
    public string? CustomerName { get; set; }
    public decimal SaleAmount { get; set; }
    public decimal MarketplaceCommission { get; set; }
    public decimal ShippingCharge { get; set; }
    public decimal TaxDeduction { get; set; }
    public decimal NetReceived { get; set; }
    public DateTime SettlementDate { get; set; }
    public string Status { get; set; } = string.Empty;
}
