
namespace Engineering.Application.Services.TransportationRequests.Models.GetPackingLogesticDetail;

public record GetPackingLogesticDetailRequest(
    long PackingId
     ) : IHttpRequest;
