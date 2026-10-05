namespace Engineering.Application.Services.RequestMachineries.Models.GetRequestMachineryProjectOperationDetail;

public record GetRequestMachineryProjectOperationDetailRequest(long RequestMachineryId,
                                                               string? FilterData,
                                                               string[]? OrderBy,
                                                               int PageIndex,
                                                               int PageSize) : IHttpRequest;