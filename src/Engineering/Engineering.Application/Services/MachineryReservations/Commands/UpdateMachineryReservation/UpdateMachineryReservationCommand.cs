using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineries;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Commands.UpdateMachineryReservation;

public record UpdateMachineryReservationCommand(
    long Id,
    RequestMachinery RequestMachinery,
    FixAssetMachinery FixAssetMachinery,
    MachineryReservationUnit MachineryReservationUnit,
    DateTime StartDate,
    DateTime EndDate,
    string? Description
    ) : ICommand<MachineryReservation>;