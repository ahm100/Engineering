namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryReturned;

public record SetRequestMachineryReturnedRequest(long RequestMachineryId,
                                                 string? Description) : IHttpRequest;
