namespace zenvy.Domain.Enums;

/// <summary>
/// Investor Status - status of investor relationships
/// </summary>
public enum InvestorStatus
{
    INACTIVE = 0,
    ACTIVE = 1,
    WITHDRAWN = 2,
    SUSPENDED = 3
}

/// <summary>
/// Investment Transaction Type
/// </summary>
public enum InvestmentTransactionType
{
    INVESTMENT = 1,
    WITHDRAWAL = 2,
    PROFIT_DISTRIBUTION = 3,
    INTEREST = 4,
    ADJUSTMENT = 5,
    PENALTY = 6
}

/// <summary>
/// Profit Distribution Status
/// </summary>
public enum ProfitDistributionStatus
{
    CALCULATED = 1,
    APPROVED = 2,
    PENDING_PAYMENT = 3,
    PAID = 4,
    PARTIAL_PAID = 5,
    FAILED = 6
}
