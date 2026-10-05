using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetAllGoodsSupplyProductDocument;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetAllGoodsSupplyProductDocument;

public record GetAllGoodsSupplyProductDocumentQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? ProductIds,
    long? CityId,
    List<GoodsSupplyType>? Types,
    List<GoodsSupplyDetailStatus>? Statuses,
    DateTime? StartDate,
    DateTime? EndDate,
    int PageIndex,
    int PageSize
    ) : IQuery<List<GetAllGoodsSupplyProductDocumentResponseModel>?>;
