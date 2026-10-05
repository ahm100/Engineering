using Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelEnum;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelExporter;

public record GetsTransportationRequestExcelExporterRequest(
    List<long>? Ids,
    List<long>? ProjectIds,
    List<long>? CostCenterIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? CostGroupIds,
    List<long>? CostCategoryIds,
    TransportationRequestStatus? TransportationRequestStatus,
    TransportationPaymentType? PaymentType,
    long? TripId,
    long? BillOfLadingId,
    long? TransportationId,
    long? RequestById,
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime? FromDate,
    DateTime? ToDate,
    decimal? FromPrice,
    decimal? ToPrice,
    string? DriverName,
    long? RequestNumber,
    string? FilterData,
    List<TransportationRequestExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
