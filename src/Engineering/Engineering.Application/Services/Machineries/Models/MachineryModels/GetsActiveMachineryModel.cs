namespace Engineering.Application.Services.Machineries.Models.MachineryModels;

public record GetsActiveMachineryModel
{
    public long Id { get; set; }
    public string MachineryName { get; set; } = string.Empty;
    public string MachineryCode { get; set; } = string.Empty;
    public long? MachineryGroupId { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public string? MachineryGroupCode { get; set; } = string.Empty;
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public long? CurrencyId { get; set; }
    public string? CurrencyName { get; set; } = string.Empty;
}
