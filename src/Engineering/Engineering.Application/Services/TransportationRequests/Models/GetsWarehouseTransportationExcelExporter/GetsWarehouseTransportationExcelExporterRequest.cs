using Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportationExcelEnum;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportationExcelExporter;

public record GetsWarehouseTransportationExcelExporterRequest(
    List<long>? Ids,
    long? ContractorId,
    List<TransportationRequestStatus>? TransportationRequestStatus,
    long? TransportationId,
    long? RequestById,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    int PageIndex,
    int PageSize,
    List<WarehouseTransportationExcelEnum>? ExcelFilters
    ) : IHttpRequest;
