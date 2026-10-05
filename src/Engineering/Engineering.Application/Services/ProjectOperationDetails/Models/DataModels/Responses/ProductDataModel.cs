using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Responses;

public record ProductDataModel
{
    public long? Id { get; set; }
    public long ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; }
    public string? ProductGroupCode { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public bool? IsActive { get; set; }
    public bool IsStandard { get; set; }
    public string IsStandardTitle => IsStandard ? "استاندارد" : "غیراستاندارد";
    public decimal? StandardValue { get; set; }
    public decimal FinalValueRes { get; set; }
    public decimal FinalValue => Math.Round(FinalValueRes, 2);
    public decimal RequestedValue { get; set; } = 0;
    public long? MeasureUnitId { get; set; }
    public string? MeasureUnitName { get; set; }
    public VolumeProductType VolumeProductType { get; set; }
    public string TypeDescription => VolumeProductType.GetEnumDescription();
}