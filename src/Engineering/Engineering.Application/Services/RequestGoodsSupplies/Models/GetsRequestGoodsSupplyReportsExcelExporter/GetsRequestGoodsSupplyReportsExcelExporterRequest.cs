using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyDetailReportsExcelEnums;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyReportsExcelEnums;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyReportsExcelExporter;

public record GetsRequestGoodsSupplyReportsExcelExporterRequest(
    List<long>? Ids,
    List<long>? DetailIds,
    long? CostCenterId,
    long? ProjectId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    List<GoodsSupplyStatus>? Statuses,
    List<long>? ProductIds,
    GoodsSupplyManagementType? Type,
    VolumeProductType? ProductType,
    long? ProductGroupId,
    DateTime? FromDate,
    DateTime? ToDate,
    long? CreatorId,
    List<RequestGoodsSupplyReportsExcelEnum>? ExcelFilters,
    List<RequestGoodsSupplyDetailReportsExcelEnum>? DetailExcelFilters,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
