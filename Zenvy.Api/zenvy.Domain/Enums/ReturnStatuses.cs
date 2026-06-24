namespace zenvy.Domain.Enums;

/// <summary>
/// Sales Return Status - represents the state of a return request
/// </summary>
public enum ReturnStatus
{
    REQUESTED = 1,
    APPROVED = 2,
    REJECTED = 3,
    PROCESSING = 4,
    COMPLETED = 5
}

/// <summary>
/// Refund Status - represents the refund payment state
/// </summary>
public enum RefundStatus
{
    PENDING = 1,
    INITIATED = 2,
    PROCESSED = 3,
    FAILED = 4,
    CANCELLED = 5
}
