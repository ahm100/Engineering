namespace Engineering.Application.Services.OperationInfos.Models.GetOIActionByOperationInfoId;

public record GetOIActionByOperationInfoIdResponse(
    List<GetOIActionByOperationInfoIdModel> Data,
    int RowCount);

public record GetOIActionByOperationInfoIdModel
{
    public long Id { get; set; }
    public long ActionId { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public string ActionCode { get; set; } = string.Empty;
}