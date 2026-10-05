namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryHistories;

public record GetRequestMachineryHistoriesResponse
{
    public long? RequestNumber { get; set; }
    public string? MachineriesGroupName { get; set; } = string.Empty;
    public string? MachineryName { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public List<GetRequestMachineryHistoriesModel>? Data { get; set; }
    public int RowCount { get; set; }
}
