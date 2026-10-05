using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Commands.Inactive;

public record InactiveTransportationCommand(
    long Id
    ) : ICommand<Transportation>;