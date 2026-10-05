namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementByIdThirdPartyModel
{
    public long Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}
