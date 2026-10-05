namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryRejected;

public record SetRequestMachineryRejectedRequest(long RequestMachineryId,
                                                 string? Description) : IHttpRequest;
