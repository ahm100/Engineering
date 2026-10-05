namespace Engineering.Application.Services.Transportations.Models.GetsTransportationExcelExporter;

public record GetsTransportationExcelExporterModel
{
    public long Id { get; set; }
    public string TransportationName { get; set; } = string.Empty;
    public string TransportationCode { get; set; } = string.Empty;
    public bool IsPassenger { get; set; }
    public bool IsActive { get; set; }
    public long? CompanyId { get; set; }
    public string? CompanyNameFa { get; set; } = string.Empty;
};
