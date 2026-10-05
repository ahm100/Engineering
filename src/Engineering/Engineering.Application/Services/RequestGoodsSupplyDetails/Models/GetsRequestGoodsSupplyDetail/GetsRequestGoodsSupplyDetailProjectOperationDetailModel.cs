namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;

public record GetsRequestGoodsSupplyDetailProjectOperationDetailModel
(
    long? Id,
    long? OperationLocationId,
    string? PrivateName,
    string? PrivateCode,
    string? PublicName,
    string? PublicCode,
    decimal? FinalAmount,
    string? Description
);