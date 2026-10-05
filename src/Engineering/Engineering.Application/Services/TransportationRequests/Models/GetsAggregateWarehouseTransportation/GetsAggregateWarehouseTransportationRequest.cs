
using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportation;

public record GetsAggregateWarehouseTransportationRequest(
    List<long>? Ids,
    List<long>? ProductIds,
    long? ContractorId,
    List<TransportationRequestStatus>? TransportationRequestStatus,
    long? TransportationId,
    long? MachineTypeId,
    long? RequestById,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FreightNumber,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
