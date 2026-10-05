namespace Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;

public record GetsFilteredBillOfLadingResponse(
    List<GetsFilteredBillOfLadingResponseModel> Data,
    int RowCount);

public record GetsFilteredBillOfLadingResponseModel
{
    public long Id { get; set; }
    public string BillOfLadingName { get; set; } = string.Empty;
    public string BillOfLadingCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}