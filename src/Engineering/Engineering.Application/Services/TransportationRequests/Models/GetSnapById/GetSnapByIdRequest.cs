
namespace Engineering.Application.Services.TransportationRequests.Models.GetSnapById;

public record GetSnapByIdRequest(
    long Id
     ) : IHttpRequest;
