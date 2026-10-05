
using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using MachineryReservation = Engineering.Domain.Entities.FixAssetMachineries.MachineryReservation;

namespace Engineering.Application.Services.MachineryReservations.Queries.GetMachineryReservations;

public record GetMachineryReservationsQuery(
    List<long>? Ids,
    List<long>? MachineryIds,
    List<long>? FixAssetMachineryIds,
    List<long>? RequestMachineryIds,
    MachineryReservationUnit? Unit,
    MachineryReservationStatus? Status,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<MachineryReservation>>>;