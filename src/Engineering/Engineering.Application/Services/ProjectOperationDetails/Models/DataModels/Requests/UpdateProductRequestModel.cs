using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record UpdateProductRequestModel
{
    public long? Id { get; set; }
    public long ProductGroupId { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public decimal FinalValue { get; set; }
    public VolumeProductType? VolumeProductType { get; set; } = Domain.Entities.ProjectOperationDetails.Enums.VolumeProductType.ProductGroup;
    public bool? IsDeleted { get; set; }
}


