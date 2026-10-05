using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Services;

public record ProductServiceModel
{
    public long ProductGroupId { get; init; }
    public string? ProductGroupName { get; init; }
    public string? ProductGroupCode { get; init; }
    public string? MeasureUnitName { get; init; }
    public decimal? UnusedPercentage { get; set; }
    public bool IsStandard { get; init; }
    public string IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public decimal? StandardValue { get; init; }
    public decimal FinalValue { get; set; }
    public VolumeProductType VolumeProductType { get; init; }
    public string TypeDescription => VolumeProductType.GetEnumDescription();
}
