using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;

public record GetsRequestGoodsSupplyDetailGroupModel
(
    long? Id,
    long? GroupId,
    VolumeProductType? ProductType,
    string? Name,
    string? Code,
    string? Measure
);
