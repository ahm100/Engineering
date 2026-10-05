namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementByIdDetailManagementWarehouseModel
{
    public long Id { get; set; }
    public string? Code { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public double? RealQuantity { get; set; }
    public int ConfirmedLoanCount { get; set; }
}