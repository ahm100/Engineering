using Engineering.Domain.Entities.FixAssetMachineries.Enums;

namespace Engineering.Application.Services.MachineryReservations.Models.UpdateMachineryReservationStatus;

public record UpdateMachineryReservationStatusRequest(
      long Id,
      MachineryReservationStatus MachineryReservationStatus
     ) : IHttpRequest;
