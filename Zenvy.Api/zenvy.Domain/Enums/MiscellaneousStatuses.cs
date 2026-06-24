namespace zenvy.Domain.Enums;

/// <summary>
/// Return Reason - reasons for product returns
/// </summary>
public enum ReturnReason
{
    DEFECTIVE = 1,
    NOT_AS_DESCRIBED = 2,
    DAMAGED_IN_TRANSIT = 3,
    WRONG_ITEM = 4,
    UNWANTED = 5,
    SIZE_FIT_ISSUE = 6,
    QUALITY_ISSUE = 7,
    OTHER = 8
}

/// <summary>
/// Product Condition - condition of returned items
/// </summary>
public enum ProductCondition
{
    NEW = 1,
    LIKE_NEW = 2,
    GOOD = 3,
    FAIR = 4,
    DAMAGED = 5
}

/// <summary>
/// Refund Method - how refund will be processed
/// </summary>
public enum RefundMethod
{
    ORIGINAL_PAYMENT_METHOD = 1,
    WALLET = 2,
    BANK_TRANSFER = 3,
    STORE_CREDIT = 4
}

/// <summary>
/// Inventory Transaction Type - for inventory movements
/// </summary>
public enum InventoryTransactionType
{
    PURCHASE = 1,
    SALE = 2,
    RETURN = 3,
    ADJUSTMENT = 4,
    DAMAGE = 5,
    TRANSFER = 6
}

/// <summary>
/// Employee Commission Status
/// </summary>
public enum CommissionStatus
{
    PENDING = 1,
    CALCULATED = 2,
    APPROVED = 3,
    PAID = 4,
    DISPUTED = 5,
    CANCELLED = 6
}
