using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;

public record ConsumableVolumeProductModel
{
    public long ProjectOperationDetailProductGroupId { get; init; }
    public long ProjectOperationDetailId { get; init; }
    public long Id { get; init; }
    public string? Name { get; init; }
    public string? Code { get; init; }
    public decimal? UnusedPercentage { get; set; }
    public bool? IsActive { get; init; }
    public bool IsStandard { get; init; }
    public string IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public decimal? StandardValue { get; init; }
    public decimal FinalValue { get; set; }
    public long? MeasureUnitId { get; init; }
    public string? MeasureUnitName { get; set; }
    public VolumeProductType VolumeProductType { get; init; }
    public string TypeDescription => VolumeProductType.GetEnumDescription();
}