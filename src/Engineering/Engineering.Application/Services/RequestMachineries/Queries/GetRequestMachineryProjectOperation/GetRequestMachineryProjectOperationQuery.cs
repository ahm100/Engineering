using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetRequestMachineryProjectOperation;

public record GetRequestMachineryProjectOperationQuery(long RequestMachineryId,
                                                       string? FilterData,
                                                       string[]? OrderBy,
                                                       int PageIndex,
                                                       int PageSize) : IQuery<DataResult<List<RequestMachineryProjectOperation>>>;