namespace zenvy.application.DTOs.Warehouses;

public class WarehouseRequest
{
    public string WarehouseName { get; set; } = string.Empty;
    public string? Location { get; set; }
    public bool Status { get; set; } = true;
}

public class WarehouseResponse
{
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
    public string? Location { get; set; }
    public bool Status { get; set; }
}

public class WarehouseDropdownResponse
{
    public int WarehouseId { get; set; }
    public string WarehouseName { get; set; } = string.Empty;
}
