namespace Engineering.Application.Services.CabinTypes.Models.CabinTypeGroupDelete;

public record CabinTypeGroupDeleteRequest(
    List<long> Ids)
    : IHttpRequest;