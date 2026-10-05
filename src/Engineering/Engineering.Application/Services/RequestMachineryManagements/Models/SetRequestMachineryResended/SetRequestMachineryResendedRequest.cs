namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryResended;

public record SetRequestMachineryResendedRequest(long RequestMachineryId,
                                                 string? Description) : IHttpRequest;
