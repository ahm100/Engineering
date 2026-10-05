
namespace Engineering.Application.Services.MachineryReservations.Models.MachineryReservationGroupDelete;

public record MachineryReservationGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
