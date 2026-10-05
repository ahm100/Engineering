using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Commands.UpdateMachineryReservationStatus;

public record UpdateMachineryReservationStatusCommand(
    long Id,
    MachineryReservationStatus MachineryReservationStatus
    ) : ICommand<MachineryReservation>;