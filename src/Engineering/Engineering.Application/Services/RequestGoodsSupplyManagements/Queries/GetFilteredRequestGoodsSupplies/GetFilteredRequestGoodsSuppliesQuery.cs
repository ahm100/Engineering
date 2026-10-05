using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.RequestGoodsSupplyManagements.Queries.GetFilteredRequestGoodsSupplies;

public record GetFilteredRequestGoodsSuppliesQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? ProjectManagerId,
    List<long>? CreatorIds,
    List<long>? ProductIds,
    DateTime? FromDate,
    DateTime? ToDate,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    List<GoodsSupplyType>? Types,
    List<GoodsSupplyType>? RemoveTypes,
    string? FilterData,
    long? companyId,
    string? CustomerInvoiceNumber,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupply>>>;
