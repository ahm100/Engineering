using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.StateChangerTransportations;

public record StateChangerTransportationsCommand(
    List<Transportation> Items,
    bool State
    ) : ICommand<bool?>;
