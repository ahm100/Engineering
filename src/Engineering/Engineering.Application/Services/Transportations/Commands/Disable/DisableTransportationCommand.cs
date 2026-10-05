using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Disable;

public record DisableTransportationCommand(
    long Id
    ) : ICommand<Transportation>;