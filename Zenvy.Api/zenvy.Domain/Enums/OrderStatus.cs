namespace zenvy.Domain.Enums;

/// <summary>
/// Sales Order Status - represents the lifecycle of a sales order
/// </summary>
public enum OrderStatus
{
    CONFIRMED = 1,
    PROCESSING = 2,
    PACKED = 3,
    SHIPPED = 4,
    DELIVERED = 5,
    CANCELLED = 6,
    PENDING = 0
}
