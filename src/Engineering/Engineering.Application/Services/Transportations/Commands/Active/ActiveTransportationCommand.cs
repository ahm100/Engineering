using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Active;

public record ActiveTransportationCommand(
    long Id
    ) : ICommand<Transportation>;