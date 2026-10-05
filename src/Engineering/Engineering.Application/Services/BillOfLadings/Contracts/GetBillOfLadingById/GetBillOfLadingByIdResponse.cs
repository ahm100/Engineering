namespace Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;

public record GetBillOfLadingByIdResponse
{
    public long Id { get; set; }
    public string BillOfLadingName { get; set; } = string.Empty;
    public string BillOfLadingCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}