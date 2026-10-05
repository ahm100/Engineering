namespace Engineering.Application.Services.RequestMachineryBills.Models.RequestMachineryBillModel;

public record BillNumberPlatesModel
{
    public string? Part1 { get; set; } = string.Empty;
    public string? Part2 { get; set; } = string.Empty;
    public string? Part3 { get; set; } = string.Empty;
    public string? Letter { get; set; } = string.Empty;
}
