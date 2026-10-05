using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyDetailExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyManagementExcelEnums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyExcelExporter;

public record GetsRequestGoodsSupplyExcelExporterRequest(
    List<long>? Ids,
    List<long>? DetailIds,
    List<long>? ManagementIds,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? CityId,
    long? ProjectManagerId,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    List<GoodsSupplyType>? Types,
    List<long>? CreatorIds,
    List<long>? ProductIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterProduct,
    string? FilterDescription,
    string? FilterPublicName,
    string? FilterOperationInfoName,
    string? FilterData,
    List<RequestGoodsSupplyExcelEnum>? ExcelFilters,
    List<RequestGoodsSupplyDetailExcelEnum>? DetailExcelFilters,
    List<RequestGoodsSupplyManagementExcelEnum>? ManagementExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
