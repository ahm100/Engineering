namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementMeasureByIdModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
