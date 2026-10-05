
namespace Engineering.Application.Services.Transportations.Models.GetById;

public record GetTransportationByIdRequest(
    long Id
     ) : IHttpRequest;
