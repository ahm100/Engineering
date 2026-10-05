using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.DataModels;

public record ProductsDataModel
{
    public long Id { get; init; }
    public long ProjectOperationDetailId { get; init; }
    public long ProductGroupId { get; init; }
    public decimal? UnusedPercentage { get; init; }
    public bool? IsActive { get; init; }
    public bool IsStandard { get; init; }
    public decimal? StandardValue { get; init; }
    public decimal FinalValue { get; set; }
    public VolumeProductType VolumeProductType { get; init; }
    public string TypeDescription => VolumeProductType.GetEnumDescription();
}