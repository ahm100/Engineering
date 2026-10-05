namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFilteredFiduciaryProductManageWarehouses;

public record GetFilteredFiduciaryProductManageWarehousesModel
{
    public long Id { get; set; }
    public string? Code { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public double? RealQuantity { get; set; }
    public bool IsDefault { get; set; }
}

