namespace Engineering.Application.Services.ServiceInfos.Models.GetServiceByCode;

public record GetServiceInfoByCodeResponse
{
    public long Id { get; set; }
    public string ServiceInfoName { get; set; } = string.Empty;
    public string ServiceInfoCode { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
}
