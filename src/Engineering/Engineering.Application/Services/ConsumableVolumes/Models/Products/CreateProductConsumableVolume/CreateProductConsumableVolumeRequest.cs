using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.CreateProductConsumableVolume;

public record CreateConsumableVolumeProductRequest : IHttpRequest
{
    public long ProjectOperationDetailId { get; set; }
    public long ProductGroupId { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public decimal FinalValue { get; set; }
    public VolumeProductType? VolumeProductType { get; set; } = Domain.Entities.ProjectOperationDetails.Enums.VolumeProductType.ProductGroup;
}


