using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Queries.GetMachineryReservationById;

public record GetMachineryReservationByIdQuery(
    long Id
    ) : IQuery<MachineryReservation?>;