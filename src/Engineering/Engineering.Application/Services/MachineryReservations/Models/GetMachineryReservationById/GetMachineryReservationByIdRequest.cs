namespace Engineering.Application.Services.MachineryReservations.Models.GetMachineryReservationById;

public record GetMachineryReservationByIdRequest(
    long Id
     ) : IHttpRequest;
