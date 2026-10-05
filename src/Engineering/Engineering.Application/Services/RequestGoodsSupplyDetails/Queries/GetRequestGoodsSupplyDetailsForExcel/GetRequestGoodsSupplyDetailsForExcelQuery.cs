using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetRequestGoodsSupplyDetailsForExcel;

public record GetRequestGoodsSupplyDetailsForExcelQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    long? CityId,
    long? ProjectManagerId,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    List<GoodsSupplyType>? Types,
    List<long>? CreatorIds,
    List<long>? ProductIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<RequestGoodsSupplyDetail>>>;
