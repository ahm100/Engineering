using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjectCategoryProductByProjectId;

public record GetProjectCategoryProductByProjectIdResponse(
    List<GetProjectCategoryProductByProjectIdModel> Data,
    int RowCount
    );

public record GetProjectCategoryProductByProjectIdModel()
{
    public long Id { get; set; }
    public long? ProductCategoryId { get; set; }
    public string? CategoryTitle { get; set; }
    public string? CategoryCode { get; set; }
    public decimal RequestQuantity { get; set; }
    public decimal RemainingQuantity { get; set; }
    public decimal InProgressQuantity { get; set; }
    public decimal TolerancePercentage { get; set; }
    public decimal CompletedQuantity { get; set; }
    public bool IsActive { get; set; }
    public bool DefaultManagerSet { get; set; } = false;
    public ProjectProductType ProductType { get; set; }
    public string ProductTypeTitle => ProductType.GetEnumDescription();
}