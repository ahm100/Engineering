namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementCostCenterByIdResponse
{
    public long Id { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
}