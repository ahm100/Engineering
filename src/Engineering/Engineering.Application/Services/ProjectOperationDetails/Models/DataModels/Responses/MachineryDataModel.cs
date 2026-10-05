using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

public record MachineryDataModel
{
    public long? Id { get; set; }
    public long MachineryId { get; set; }
    public string? MachineryName { get; set; }
    public string? MachineryCode { get; set; }
    public decimal Number { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public bool IsStandard { get; set; }
    public string IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public string? StandardValue { get; set; }
    public decimal FinalValueDecimal { get; set; }
    public string? FinalValue { get; set; }
    public RequestMachineryUnit? Unit { get; set; }
    public string? UnitDescription => Unit?.GetEnumDescription();
}