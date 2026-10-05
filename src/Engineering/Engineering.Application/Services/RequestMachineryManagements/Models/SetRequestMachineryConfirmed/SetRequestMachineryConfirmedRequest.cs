namespace Engineering.Application.Services.RequestMachineryManagements.Models.SetRequestMachineryConfirmed;

public record SetRequestMachineryConfirmedRequest(long RequestMachineryId,
                                                  long OperatorAppoinmentId,
                                                  string? ConfirmedTimeRequired,
                                                  string? Description) : IHttpRequest;
