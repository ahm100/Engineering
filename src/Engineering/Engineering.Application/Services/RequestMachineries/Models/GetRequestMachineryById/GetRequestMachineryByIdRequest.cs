namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryById;

public record GetRequestMachineryByIdRequest(long RequestMachineryId) : IHttpRequest;
