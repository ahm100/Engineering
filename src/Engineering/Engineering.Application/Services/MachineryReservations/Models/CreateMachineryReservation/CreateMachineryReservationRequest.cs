using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.MachineryReservations.Models.CreateMachineryReservation;

public record CreateMachineryReservationRequest(
    long RequestMachineryId,
    long FixAssetMachineryId,
    MachineryReservationUnit MachineryReservationUnit,
    DateTime StartDate,
    DateTime EndDate,
    TimeSpan? StartTime,
    TimeSpan? EndTime,
    string? ConfirmedTimeRequired,
    string? Description
     ) : IHttpRequest;
