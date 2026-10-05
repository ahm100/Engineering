using Engineering.Application.Services.TransportationRequests.Models.CalculatePriceOfTransport;
using Engineering.Domain.Entities.Logistics;
using TransportationRequest = Engineering.Domain.Entities.Transportations.TransportationRequest;

namespace Engineering.Application.Services.TransportationRequests.Commands.CalculatePriceOfTransport;

public record CalculatePriceOfTransportCommand(
    TransportationRequest TransportationRequest,
    decimal? LoadWeight,
    List<TransportationContractorPriceWeight>? PriceWeights
    ) : ICommand<CalculatePriceOfTransportResponse?>;