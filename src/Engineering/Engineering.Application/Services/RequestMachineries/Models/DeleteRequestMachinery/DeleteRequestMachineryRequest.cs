namespace Engineering.Application.Services.RequestMachineries.Models.DeleteRequestMachinery;

public record DeleteRequestMachineryRequest(long RequestMachineryId) : IHttpRequest;
