namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementCurrencyByIdModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
