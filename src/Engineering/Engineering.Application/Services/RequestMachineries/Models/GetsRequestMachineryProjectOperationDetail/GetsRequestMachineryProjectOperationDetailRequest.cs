
namespace Engineering.Application.Services.RequestMachineries.Models.GetsRequestMachineryProjectOperationDetail;

public record GetsRequestMachineryProjectOperationDetailRequest(long RequestMachineryId,
                                                                long ProjectOperationId,
                                                                string? FilterData,
                                                                string[]? OrderBy,
                                                                int PageIndex,
                                                                int PageSize) : IHttpRequest;