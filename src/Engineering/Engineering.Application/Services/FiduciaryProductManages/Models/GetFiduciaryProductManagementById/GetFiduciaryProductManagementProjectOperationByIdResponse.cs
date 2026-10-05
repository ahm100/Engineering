namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementProjectOperationByIdResponse
{
    public long Id { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
}
