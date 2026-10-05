using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductById;

public record GetFiduciaryProductManagementByIdResponse
{
    public long Id { get; set; }
    public long? RequestNumber { get; set; }
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public long? ThirdPartyId { get; set; }
    public string? ThirdParty { get; set; } = string.Empty;
    public string? Description { get; set; }
    public FiduciaryProductStatus Status { get; set; }
    public string StatusTitle => Status.GetEnumDescription();
    public long? CreatorId { get; set; }
    public string? Creator { get; set; } = string.Empty;
    public DateTime Created { get; set; }
    public string? CreatedShamsi => TimeCalculator.ConvertToShamsi(Created);
    public List<GetFiduciaryProductManagementByIdModel>? Details { get; set; }
}
