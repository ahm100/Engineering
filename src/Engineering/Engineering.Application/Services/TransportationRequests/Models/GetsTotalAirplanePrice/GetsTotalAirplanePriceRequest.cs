using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsTotalAirplanePrice;

public record GetsTotalAirplanePriceRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? CostGroupIds,
    List<long>? CostCategoryIds,
    List<long>? TripIds,
    List<long>? TransportationIds,
    List<long>? PassengerIds,
    long? RequesterById,
    TransportationRequestStatus? TransportationRequestStatus,
    TransportationPaymentType? PaymentType,
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime? FromCreateDate,
    DateTime? ToCreateDate,
    long? RequestNumber,
    decimal? FromPrice,
    decimal? ToPrice,
    string? DriverName,
    string? FilterData
     ) : IHttpRequest;
