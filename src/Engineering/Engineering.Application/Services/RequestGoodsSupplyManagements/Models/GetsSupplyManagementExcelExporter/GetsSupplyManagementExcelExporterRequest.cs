using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelEnums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsSupplyManagementExcelExporter;

public record GetsSupplyManagementExcelExporterRequest(
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
    List<SupplyManagementExcelEnum>? ExcelFilters,
    string? CustomerInvoiceNumber,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
