# Enum Integration Guide

## Overview
Enums have been added to enforce data consistency and prevent arbitrary string values in the database. All enums are located in `zenvy.Domain.Enums`.

## Available Enums

### Order & Sales Management
- **OrderStatus**: PENDING, CONFIRMED, PROCESSING, PACKED, SHIPPED, DELIVERED, CANCELLED
- **ReturnStatus**: REQUESTED, APPROVED, REJECTED, PROCESSING, COMPLETED
- **RefundStatus**: PENDING, INITIATED, PROCESSED, FAILED, CANCELLED
- **ShipmentStatus**: PENDING, PROCESSING, PICKED, PACKED, SHIPPED, IN_TRANSIT, DELIVERED, FAILED, RETURNED, CANCELLED
- **PaymentStatus**: PENDING, INITIATED, AUTHORIZED, CAPTURED, COMPLETED, FAILED, CANCELLED, REFUNDED

### Purchase & Supply Chain
- **PurchaseOrderStatus**: DRAFT, SUBMITTED, CONFIRMED, RECEIVED, INVOICED, PAID, CANCELLED, PENDING
- **InventoryTransactionType**: PURCHASE, SALE, RETURN, ADJUSTMENT, DAMAGE, TRANSFER

### Marketplace
- **ChannelType**: MEESHO, MYNTRA, AMAZON, WEBSITE, WALK_IN, OTHER
- **MarketplaceSettlementStatus**: PENDING, PROCESSED, SETTLED, FAILED, DISPUTED, REVERSED
- **TransactionType**: SALE, RETURN, REFUND, EXPENSE, SETTLEMENT, INVESTMENT, WITHDRAWAL, COMMISSION, ADJUSTMENT

### Returns & Products
- **ReturnReason**: DEFECTIVE, NOT_AS_DESCRIBED, DAMAGED_IN_TRANSIT, WRONG_ITEM, UNWANTED, SIZE_FIT_ISSUE, QUALITY_ISSUE, OTHER
- **ProductCondition**: NEW, LIKE_NEW, GOOD, FAIR, DAMAGED
- **RefundMethod**: ORIGINAL_PAYMENT_METHOD, WALLET, BANK_TRANSFER, STORE_CREDIT

### Employee & Commission
- **CommissionStatus**: PENDING, CALCULATED, APPROVED, PAID, DISPUTED, CANCELLED

### Investment Management
- **InvestorStatus**: INACTIVE, ACTIVE, WITHDRAWN, SUSPENDED
- **InvestmentTransactionType**: INVESTMENT, WITHDRAWAL, PROFIT_DISTRIBUTION, INTEREST, ADJUSTMENT, PENALTY
- **ProfitDistributionStatus**: CALCULATED, APPROVED, PENDING_PAYMENT, PAID, PARTIAL_PAID, FAILED

### Generic
- **EntityStatus**: INACTIVE, ACTIVE, ARCHIVED, SUSPENDED

## Usage Examples

### In DTOs
```csharp
using zenvy.Domain.Enums;

public class OrderDto
{
    public long OrderId { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.CONFIRMED;
    public ShipmentStatus ShipmentStatus { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
}
```

### In Services
```csharp
public async Task<OrderDto> CreateOrderAsync(CreateOrderRequest request)
{
    var order = new Order
    {
        Status = OrderStatus.CONFIRMED,
        ShipmentStatus = ShipmentStatus.PENDING,
        PaymentStatus = PaymentStatus.PENDING
    };
    
    await _repository.AddAsync(order);
    return MapToDto(order);
}

public async Task<OrderDto> UpdateOrderStatusAsync(long orderId, OrderStatus newStatus)
{
    var order = await _repository.GetByIdAsync(orderId);
    order.Status = newStatus;
    await _repository.UpdateAsync(order);
    return MapToDto(order);
}
```

### Converting to String for Database
```csharp
// Option 1: Direct string conversion
string statusString = order.Status.ToString(); // "CONFIRMED"

// Option 2: Using EnumMappings
string statusString = EnumMappings.GetOrderStatusValue(order.Status);

// Option 3: Using EnumHelper
string statusString = EnumHelper.ToStringValue(order.Status);

// Option 4: Direct dictionary lookup
string statusString = EnumMappings.OrderStatusMap[order.Status]; // "CONFIRMED"
```

### Converting from String to Enum
```csharp
// Option 1: Using EnumHelper
if (EnumHelper.TryFromStringValue<OrderStatus>("DELIVERED", out var status))
{
    order.Status = status; // OrderStatus.DELIVERED
}

// Option 2: Direct parse (throws exception if invalid)
var status = EnumHelper.FromStringValue<OrderStatus>("DELIVERED");

// Option 3: Direct Enum.Parse
var status = (OrderStatus)Enum.Parse(typeof(OrderStatus), "DELIVERED", ignoreCase: true);
```

### Validation
```csharp
// Check if string is a valid enum value
bool isValid = EnumHelper.IsValidEnumValue<OrderStatus>("DELIVERED"); // true
bool isValid = EnumHelper.IsValidEnumValue<OrderStatus>("INVALID_STATUS"); // false

// Get all valid values
var validStatuses = EnumHelper.GetAllValues<OrderStatus>();
// Returns: ["PENDING", "CONFIRMED", "PROCESSING", "PACKED", "SHIPPED", "DELIVERED", "CANCELLED"]
```

## Stored Procedure Updates

When updating stored procedures to use these enums:

### Current (String-based)
```sql
CREATE PROCEDURE usp_CreateSalesOrder
    @Status NVARCHAR(50) = 'CONFIRMED'
AS
WHERE Status IN ('PENDING', 'CONFIRMED', 'PROCESSING', 'DELIVERED', 'CANCELLED')
```

### Recommended (With validation)
```sql
CREATE PROCEDURE usp_CreateSalesOrder
    @Status NVARCHAR(50) = 'CONFIRMED'
AS
BEGIN
    IF @Status NOT IN ('PENDING', 'CONFIRMED', 'PROCESSING', 'PACKED', 'SHIPPED', 'DELIVERED', 'CANCELLED')
        THROW 50001, 'Invalid order status', 1;
    
    INSERT INTO SalesOrders (..., Status, ...) VALUES (..., @Status, ...);
END
```

## Migration Strategy

### Phase 1: Add Enums (✓ COMPLETE)
- ✓ Created all enum definitions
- ✓ Created helper utilities (EnumHelper, EnumMappings)
- ✓ Build verification passed

### Phase 2: Update DTOs (RECOMMENDED NEXT)
1. Update OrderDtos.cs to use `OrderStatus` instead of `string`
2. Update ReturnDtos.cs to use `ReturnStatus` and `RefundStatus`
3. Update MarketplaceSettlementDtos.cs to use `MarketplaceSettlementStatus`
4. Update PurchaseOrderDtos.cs to use `PurchaseOrderStatus`
5. Update ShipmentDtos.cs to use `ShipmentStatus`
6. Update other DTOs similarly

### Phase 3: Update Services
1. Update services to handle enum conversions
2. Add validation for enum values before database operations

### Phase 4: Update Repositories
1. Update SQL reader mappings to convert strings to enums
2. Update SQL parameter passing to convert enums to strings

### Phase 5: Update Stored Procedures
1. Add validation checks for valid enum values
2. Add comments documenting accepted values

## Benefits

✓ **Type Safety**: Compiler catches invalid status values
✓ **IntelliSense**: IDE shows available enum options
✓ **Validation**: Database constraints prevent invalid data
✓ **Consistency**: Standardized status values across application
✓ **Maintainability**: Single source of truth for valid values
✓ **Documentation**: Enums serve as API documentation

## Helper Classes

### EnumHelper
Static utility class with methods:
- `ToStringValue<T>()` - Convert enum to string
- `FromStringValue<T>()` - Convert string to enum (throws exception)
- `TryFromStringValue<T>()` - Safe string to enum conversion
- `GetAllValues<T>()` - Get all valid values for enum
- `IsValidEnumValue<T>()` - Check if string is valid enum value

### EnumMappings
Static class with:
- Pre-defined dictionaries mapping enums to display strings
- Helper methods for each major enum type
- Useful for API responses and reports

## Next Steps

1. Update existing DTOs to use these enums
2. Add JSON converters for API serialization/deserialization
3. Update stored procedures with validation
4. Create validation attributes for DTOs
5. Add error handling for invalid enum conversions
