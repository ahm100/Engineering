namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryMachineries;

public record GetRequestMachineryMachineriesModel
{
    public long? MachineryGroupId { get; set; }
    public string? MachineryGroupName { get; set; } = string.Empty;
    public long? MachineryId { get; set; }
    public string? MachineryName { get; set; } = string.Empty;
    public long? ContractorId { get; set; }
    public string? Contractor { get; set; }
    public decimal? MachineryPrice { get; set; }
}