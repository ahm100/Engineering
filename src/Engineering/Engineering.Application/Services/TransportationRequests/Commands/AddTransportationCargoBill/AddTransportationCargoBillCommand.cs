using Engineering.Domain.Entities.Transportations;

namespace Engineering.Application.Services.TransportationCargos.Commands.AddTransportationCargoBill;

public record AddTransportationCargoBillCommand(
    TransportationCargo TransportationCargo,
    List<string>? DocumentUrls
    ) : ICommand<TransportationCargo>;