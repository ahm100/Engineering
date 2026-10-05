using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetFilteredDailyProjectOperationDetails;

public record GetFilteredDailyProjectOperationDetailsQuery(long ProjectOperationId,
                                                           ProjectOperationDetailStatus? Status,
                                                           DateTime? StartDate,
                                                           DateTime? EndDate,
                                                           DateTime? CreateDate,
                                                           List<long>? OperationLocationIds,
                                                           List<long>? ServiceInfoIds,
                                                           long? ContractorId,
                                                           string? FilterData,
                                                           string[]? OrderBy,
                                                           int PageIndex,
                                                           int PageSize) : IQuery<DataResult<List<ProjectOperationDetail>>>;
