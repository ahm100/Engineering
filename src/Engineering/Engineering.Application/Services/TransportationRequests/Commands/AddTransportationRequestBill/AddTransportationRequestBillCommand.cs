using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.AddTransportationRequestBill;

public record AddTransportationRequestBillCommand(
    TransportationRequest TransportationRequest,
    List<string>? DocumentUrls
    ) : ICommand<TransportationRequest>;