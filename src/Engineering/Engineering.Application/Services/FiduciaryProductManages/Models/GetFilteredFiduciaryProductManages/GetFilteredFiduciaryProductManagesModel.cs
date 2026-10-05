using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFilteredFiduciaryProductManages;

public record GetFilteredFiduciaryProductManagesModel
{
    public long Id { get; set; }
    public int RequestNumber { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string OperationInfoName { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string ThirdParty { get; set; } = string.Empty;
    public string Creator { get; set; } = string.Empty;
    public FiduciaryProductStatus Status { get; set; }
    public string StatusDescription => Status.GetEnumDescription();
}
