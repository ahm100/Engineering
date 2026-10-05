namespace Engineering.Application.Services.MachineryReservations.Models.DisableMachineryReservation;

public record DisableMachineryReservationRequest(
    long Id
     ) : IHttpRequest;
