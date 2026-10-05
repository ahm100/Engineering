using Engineering.Domain.Entities.Machineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Services;

public record MachineryServiceModel
{
    public required Machinery Machinery { get; set; }
    public decimal? Number { get; init; }
    public decimal? UnusedPercentage { get; init; }
    public bool IsStandard { get; init; }
    public string? IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public long? StandardValue { get; set; }
    public decimal FinalValue { get; set; }
    public RequestMachineryUnit? Unit { get; set; }
}
