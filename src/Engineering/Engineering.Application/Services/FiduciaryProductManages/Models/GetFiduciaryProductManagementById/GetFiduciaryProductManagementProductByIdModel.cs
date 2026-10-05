namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementProductByIdModel
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
}
