
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportation;

public record GetsWarehouseTransportationRequest(
    List<long>? Ids,
    long? ContractorId,
    List<TransportationRequestStatus>? TransportationRequestStatus,
    long? TransportationId,
    long? RequestById,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
