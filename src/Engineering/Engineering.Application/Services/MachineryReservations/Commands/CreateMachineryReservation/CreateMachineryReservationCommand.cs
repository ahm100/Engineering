using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;
using RequestMachinery = Engineering.Domain.Entities.RequestMachineries.RequestMachinery;

namespace Engineering.Application.Services.MachineryReservations.Commands.CreateMachineryReservation;

public record CreateMachineryReservationCommand(
    RequestMachinery RequestMachinery,
    FixAssetMachinery FixAssetMachinery,
    MachineryReservationUnit MachineryReservationUnit,
    DateTime StartDate,
    DateTime EndDate,
    string? Description
    ) : ICommand<MachineryReservation?>;