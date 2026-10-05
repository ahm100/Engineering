namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryDone;

public record SetRequestMachineryDoneRequest(long RequestMachineryId,
                                             string? Description) : IHttpRequest;
