using Engineering.Application.Services.TransportationRequests.Models.GetsTotalTransportationRequestPrice;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsTotalTransportationRequestPrice;

public record GetsTotalTransportationRequestPriceQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
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
    long? RequestNumber,
    decimal? FromPrice,
    decimal? ToPrice,
    string? DriverName,
    List<long>? DriverIds,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<GetsTotalTransportationRequestPriceResponse>;
