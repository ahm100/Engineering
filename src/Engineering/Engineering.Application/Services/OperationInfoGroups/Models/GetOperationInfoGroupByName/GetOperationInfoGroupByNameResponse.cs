namespace Engineering.Application.Services.OperationInfoGroups.Models.GetOperationInfoGroupByName;

public record GetOperationInfoGroupByNameResponse
{
    public long Id { get; set; }
    public string OperationInfoGroupCode { get; set; } = string.Empty;
    public string OperationInfoGroupTitle { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
