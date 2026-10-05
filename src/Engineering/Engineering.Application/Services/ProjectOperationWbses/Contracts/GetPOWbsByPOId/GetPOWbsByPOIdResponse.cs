namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByPOId;

public record GetPOWbsByPOIdResponse(
    List<GetPOWbsByPOIdModel> Data,
    int RowCount);

public class GetPOWbsByPOIdModel
{
    public long Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public long ProjectWbsId { get; set; }
    public string TitleFa { get; set; } = string.Empty;
    public string? TitleEn { get; set; } = string.Empty;
    public string? DescriptionFa { get; set; } = string.Empty;
    public string? DescriptionEn { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string? ProjectOperationDescription { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public bool IsActive { get; set; }
}