using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryProjectOperationDetail;

public record GetRequestMachineryProjectOperationDetailQuery(long RequestMachineryId,
                                                             string? FilterData,
                                                             string[]? OrderBy,
                                                             int PageIndex,
                                                             int PageSize) : IQuery<DataResult<List<RequestMachineryProjectOperationDetail>>>;