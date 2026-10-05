using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Commands.DisableMachineryReservation;

public record DisableMachineryReservationCommand(
    long Id
    ) : ICommand<MachineryReservation>;