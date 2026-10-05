using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailsExcelEnums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyDetailsExcelExporter;

public record GetRequestGoodsSupplyDetailsExcelExporterRequest(
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
    string? CustomerInvoiceNumber,
    string? FilterData,
    List<GoodsSupplyDetailExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
