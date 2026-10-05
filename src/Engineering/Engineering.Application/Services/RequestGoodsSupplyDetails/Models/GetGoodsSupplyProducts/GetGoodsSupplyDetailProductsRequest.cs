using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;

public record GetGoodsSupplyDetailProductsRequest(
    VolumeProductType? Type,
    long? ProjectOperationId,
    long? ProjectId,
    long? ProjectOperationDetailId,
    string? FilterData,
    string? ProdutFilterData,
    string? GroupFilterData,
    long? ContractorId,
    bool IsProjectSupply,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
