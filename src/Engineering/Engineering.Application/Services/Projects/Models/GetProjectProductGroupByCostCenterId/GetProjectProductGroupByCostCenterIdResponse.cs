using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjectProductGroupByCostCenterId;

public record GetProjectProductGroupByCostCenterIdResponse(
    List<GetProjectProductModel> Data,
    int RowCount
    );

public record GetProjectProductModel
{
    public long Id { get; set; }
    public long ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; } = string.Empty;
    public string? ProductGroupCode { get; set; } = string.Empty;
    public decimal? RequestQuantity { get; set; }
    public decimal? RemainingQuantity { get; set; }
    public decimal? CompletedQuantity { get; set; }
    public decimal? InProgressQuantity { get; set; }
    public bool? IsActive { get; set; }
    public bool DefaultManagerSet { get; set; } = false;
    public decimal? TolerancePercentage { get; set; }
    public ProjectProductType ProjectProductType => ProjectProductType.ProductGroup;
    public string? ProjectProductTypeTitle => ProjectProductType!.GetEnumDescription();
}
