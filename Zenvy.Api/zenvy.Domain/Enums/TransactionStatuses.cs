namespace zenvy.Domain.Enums;

/// <summary>
/// Purchase Order Status - represents the procurement lifecycle
/// </summary>
public enum PurchaseOrderStatus
{
    DRAFT = 0,
    SUBMITTED = 1,
    CONFIRMED = 2,
    RECEIVED = 3,
    INVOICED = 4,
    PAID = 5,
    CANCELLED = 6,
    PENDING = 7
}

/// <summary>
/// Marketplace Settlement Status - represents settlement payment state
/// </summary>
public enum MarketplaceSettlementStatus
{
    PENDING = 1,
    PROCESSED = 2,
    SETTLED = 3,
    FAILED = 4,
    DISPUTED = 5,
    REVERSED = 6
}

/// <summary>
/// Transaction Type - for tracking different kinds of transactions
/// </summary>
public enum TransactionType
{
    SALE = 1,
    RETURN = 2,
    REFUND = 3,
    EXPENSE = 4,
    SETTLEMENT = 5,
    INVESTMENT = 6,
    WITHDRAWAL = 7,
    COMMISSION = 8,
    ADJUSTMENT = 9
}
