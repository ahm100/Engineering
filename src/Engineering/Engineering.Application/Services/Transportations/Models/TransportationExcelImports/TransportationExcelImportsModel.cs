using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.Transportations.Models.TransportationExcelImports;

public record TransportationExcelImportsModel
{
    public string TransportationName { get; private set; } = string.Empty;
    public string TransportationCode { get; private set; } = string.Empty;
    public bool IsPassenger { get; private set; } = false;
    public bool IsActive { get; private set; } = true;
    public TransportationType? TransportationType { get; set; }
}
