using Engineering.Application.Services.TransportationRequests.Models.UpdateTransportPalletLoadWeight;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Commands.UpdateTransportPalletLoadWeight;

public record UpdateTransportPalletLoadWeightCommand(
    List<TransportPalletWeightsModel> Pallets
    ) : ICommand<List<TransportationCargoPallet>>;