using Engineering.Application.Services.TransportationRequests.Models.GetsFilteredAirplane;
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Queries.GetsAirplaneFiltered;

public record GetsFilteredAirplaneQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? CostGroupIds,
    List<long>? CostCategoryIds,
    List<long>? TripIds,
    List<long>? TransportationIds,
    List<long>? PassengerIds,
    long? RequestById,
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
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsFilteredAirplaneResponseModel>>>;
