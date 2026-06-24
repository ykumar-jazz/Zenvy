namespace zenvy.Domain.Enums;

/// <summary>
/// Sales Channel Type - represents different sales channels
/// </summary>
public enum ChannelType
{
    MEESHO = 1,
    MYNTRA = 2,
    AMAZON = 3,
    WEBSITE = 4,
    WALK_IN = 5,
    OTHER = 6
}

/// <summary>
/// Shipment Status - represents shipment lifecycle
/// </summary>
public enum ShipmentStatus
{
    PENDING = 1,
    PROCESSING = 2,
    PICKED = 3,
    PACKED = 4,
    SHIPPED = 5,
    IN_TRANSIT = 6,
    DELIVERED = 7,
    FAILED = 8,
    RETURNED = 9,
    CANCELLED = 10
}

/// <summary>
/// Payment Status - represents payment state
/// </summary>
public enum PaymentStatus
{
    PENDING = 1,
    INITIATED = 2,
    AUTHORIZED = 3,
    CAPTURED = 4,
    COMPLETED = 5,
    FAILED = 6,
    CANCELLED = 7,
    REFUNDED = 8
}

/// <summary>
/// Entity Status - generic active/inactive status
/// </summary>
public enum EntityStatus
{
    INACTIVE = 0,
    ACTIVE = 1,
    ARCHIVED = 2,
    SUSPENDED = 3
}
