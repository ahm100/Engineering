using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyProductGroups;

public record GetRequestGoodsSupplyProductGroupsResponseModel
{
    public long? ProductGroupId { get; set; }
    public string? ProductGroupName { get; set; } = string.Empty;
    public string? ProductGroupCode { get; set; } = string.Empty;
    public VolumeProductType? ProductType { get; set; }
    public string? ProductTypeDescription => ProductType?.GetEnumDescription();
};
