namespace Engineering.Application.Services.DailyProjectOperations.Models.GetFilteredMachineries;

public record GetFilteredMachineriesResponseModel
{
    public long MachineryId { get; set; }
    public long MachineryGroupId { get; set; }
    public string? MachineryCode { get; set; } = string.Empty;
    public string? MachineryName { get; set; } = string.Empty;
    public string? MachineryGroupName { get; set; } = string.Empty;
    public decimal? Number { get; set; }
    public long? ConsumableVolumeMachineryId { get; set; }
    public List<GetFilteredRequestMachineriesResponseModel> RequestMachineries { get; set; } = new();
}
