namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;

public record GetsActiveBillOfLadingResponse(
    List<GetsActiveBillOfLadingResponseModel> Data,
    int RowCount);

public record GetsActiveBillOfLadingResponseModel
{
    public long Id { get; set; }
    public string BillOfLadingName { get; set; } = string.Empty;
    public string BillOfLadingCode { get; set; } = string.Empty;
}