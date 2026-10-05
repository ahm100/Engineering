using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservation;

public record UpdateMachineryReservationRequest(
    long Id,
    long RequestMachineryId,
    long FixAssetMachineryId,
    MachineryReservationUnit MachineryReservationUnit,
    DateTime StartDate,
    DateTime EndDate,
    TimeSpan? StartTime,
    TimeSpan? EndTime,
    string? Description
     ) : IHttpRequest;
