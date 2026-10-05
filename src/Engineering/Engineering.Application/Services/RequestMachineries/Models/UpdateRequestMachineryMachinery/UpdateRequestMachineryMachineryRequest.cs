namespace Engineering.Application.Services.RequestMachineries.Models.UpdateRequestMachineryMachinery;

public record UpdateRequestMachineryMachineryRequest(long RequestMachineryId,
                                                     long MachineryId) : IHttpRequest;
