using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Application.Services.Projects.Models.GetProjectById;

public record GetProjectProductByProjectIdResponse(
    List<GetProjectProductByProjectIdModel> Data,
    int RowCount
    );

public record GetProjectProductByProjectIdModel()
{
    public long Id { get; set; }
    public long? ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; }
    public string? ProductGroupCode { get; set; }
    public decimal RequestQuantity { get; set; }
    public decimal RemainingQuantity { get; set; }
    public decimal InProgressQuantity { get; set; }
    public bool DefaultManagerSet { get; set; }
    public decimal CompletedQuantity { get; set; }
    public ProjectProductType ProductType { get; set; }
}