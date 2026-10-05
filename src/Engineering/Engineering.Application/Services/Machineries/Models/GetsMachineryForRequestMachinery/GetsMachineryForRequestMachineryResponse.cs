
namespace Engineering.Application.Services.Machineries.Models.GetsMachineryForRequestMachinery;

public record GetsMachineryForRequestMachineryResponse(
    List<GetsMachineryForRequestMachineryModel> Data,
    int RowCount);

public record GetsMachineryForRequestMachineryModel
{
    public long Id { get; set; }
    public string MachineryName { get; set; } = string.Empty;
    public string MachineryCode { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
