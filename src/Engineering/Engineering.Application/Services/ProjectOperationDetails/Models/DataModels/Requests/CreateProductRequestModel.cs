using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.DataModels.Requests;

public record CreateProductRequestModel
{
    public long ProductGroupId { get; set; }
    public decimal? UnusedPercentage { get; set; }
    public decimal FinalValue { get; set; }
    public decimal? StandardValue { get; set; }
    public VolumeProductType? VolumeProductType { get; set; } = Domain.Entities.ProjectOperationDetails.Enums.VolumeProductType.ProductGroup;
}


