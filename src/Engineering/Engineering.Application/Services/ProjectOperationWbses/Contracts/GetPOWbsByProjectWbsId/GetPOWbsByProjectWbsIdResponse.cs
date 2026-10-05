namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetPOWbsByProjectWbsId;

public record GetPOWbsByProjectWbsIdResponse(
    List<GetPOWbsByProjectWbsIdModel> Data,
    int RowCount);

public class GetPOWbsByProjectWbsIdModel
{
    public long Id { get; set; }
    public long ProjectOperationId { get; set; }
    public string? ProjectOperationDescription { get; set; }
    public long OperationInfoId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public long? CreatorId { get; set; }
    public string? Creator { get; set; }
    public bool IsActive { get; set; }
}