using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservations;

public record GetMachineryReservationsRequest(
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
     ) : IHttpRequest;
