using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.UpdateProductConsumableVolume;

public record UpdateConsumableVolumeProductRequest : IHttpRequest
{
    public long Id { get; set; }
    public long ProductGroupId { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public decimal FinalValue { get; set; }
    public VolumeProductType? VolumeProductType { get; set; } = Domain.Entities.ProjectOperationDetails.Enums.VolumeProductType.ProductGroup;
}
