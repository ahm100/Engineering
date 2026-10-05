namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetProjectOperationWbsById;

public class GetProjectOperationWbsByIdResponse
{
    public long Id { get; set; }
    public long ProjectWbsId { get; set; }
    public string Code { get; set; } = string.Empty;
    public string TitleFa { get; set; } = string.Empty;
    public string? TitleEn { get; set; } = string.Empty;
    public string? DescriptionFa { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string? ProjectOperationDescription { get; set; } = string.Empty;
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public bool IsActive { get; set; }
}