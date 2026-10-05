using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjectProductCategoryByCostCenterId;

public record GetProjectProductCategoryByCostCenterIdResponse(
    List<GetProjectProductCategoryByCostCenterIdModel> Data,
    int RowCount
    );

public record GetProjectProductCategoryByCostCenterIdModel
{
    public long Id { get; set; }
    public long CategoryId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public ProjectProductType ProjectProductType => ProjectProductType.Category;
    public string? ProjectProductTypeTitle => ProjectProductType!.GetEnumDescription();
}
