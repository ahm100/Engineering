using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.Transportations.Models.GetsActive;

public record GetsActiveTransportationResponseModel
{
    public long Id { get; set; }
    public string TransportationName { get; set; } = string.Empty;
    public string TransportationCode { get; set; } = string.Empty;
    public bool IsPassenger { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
    public TransportationType? TransportationType { get; set; }
    public string? TransportationTypeDescription => TransportationType?.GetEnumDescription();
}

