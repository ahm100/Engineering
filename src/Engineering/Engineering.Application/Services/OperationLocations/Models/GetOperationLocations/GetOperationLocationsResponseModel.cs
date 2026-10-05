
namespace Engineering.Application.Services.OperationLocations.Models.GetOperationLocations;

public record GetOperationLocationsResponseModel
{
    public long Id { get; set; }
    public string PrivateName { get; set; } = string.Empty;
    public string PrivateCode { get; set; } = string.Empty;
    public string PublicName { get; set; } = string.Empty;
    public string PublicCode { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public string Coding { get; set; } = string.Empty;
    public long? ParentId { get; set; }
    public long? CostCenterId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public int? Priority { get; set; }
    public bool IsActive { get; set; }
    public bool HaveChild { get; set; }
    public int ChildCount { get; set; }
    public List<GetOperationLocationsResponseModel>? Childs { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
