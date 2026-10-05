using Engineering.Domain.Entities.Transportations.Enums;

namespace Engineering.Application.Services.TransportationRequests.Models.GroupTransportationRequestStatusChanger;

public record GroupTransportationRequestStatusChangerRequest(
    List<long> Ids,
    TransportationRequestStatus Status,
    string? ManagerDescription
     ) : IHttpRequest;
