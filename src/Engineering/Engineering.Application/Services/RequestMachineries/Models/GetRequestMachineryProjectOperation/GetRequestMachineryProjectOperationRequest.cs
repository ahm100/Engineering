namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryProjectOperation;

public record GetRequestMachineryProjectOperationRequest(long RequestMachineryId,
                                                         string? FilterData,
                                                         string[]? OrderBy,
                                                         int PageIndex,
                                                         int PageSize) : IHttpRequest;