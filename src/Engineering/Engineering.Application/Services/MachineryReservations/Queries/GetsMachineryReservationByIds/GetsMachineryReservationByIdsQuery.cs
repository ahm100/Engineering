using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Queries.GetsMachineryReservationByIds;

public record GetsMachineryReservationByIdsQuery(
    List<long> ids,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<MachineryReservation?>>>;