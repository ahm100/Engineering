using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetProjectOperationDetailByProjectOperation;

public record GetProjectOperationDetailByProjectOperationQuery(long RequestMachineryId,
                                                               long ProjectOperationId,
                                                               string? FilterData,
                                                               string[]? OrderBy,
                                                               int PageIndex,
                                                               int PageSize) : IQuery<DataResult<List<RequestMachineryProjectOperationDetail>>>;