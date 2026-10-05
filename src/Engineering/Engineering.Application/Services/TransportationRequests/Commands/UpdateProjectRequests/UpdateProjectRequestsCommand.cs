using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateProjectRequests;

public record UpdateProjectRequestsCommand(
    List<TransportationRequestProject> Projects,
    TransportationRequest TransportationRequest
    ) : ICommand<TransportationRequest>;