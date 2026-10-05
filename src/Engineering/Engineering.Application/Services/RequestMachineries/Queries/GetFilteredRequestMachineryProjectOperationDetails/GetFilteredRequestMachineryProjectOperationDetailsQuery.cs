using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.RequestMachineries.Queries.GetFilteredRequestMachineryProjectOperationDetails;

public record GetFilteredRequestMachineryProjectOperationDetailsQuery(long ProjectOperationDetailId,
                                                                      string? FilterData,
                                                                      string[]? OrderBy,
                                                                      int PageIndex,
                                                                      int PageSize) : IQuery<DataResult<List<RequestMachinery>>>;
