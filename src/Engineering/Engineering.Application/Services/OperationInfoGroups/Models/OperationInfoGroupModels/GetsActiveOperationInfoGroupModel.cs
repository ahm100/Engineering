namespace Engineering.Application.Services.OperationInfoGroups.Models.OperationInfoGroupModels;

public record GetsActiveOperationInfoGroupModel
{
    public long Id { get; set; }
    public string OperationInfoGroupCode { get; set; } = string.Empty;
    public string OperationInfoGroupTitle { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}