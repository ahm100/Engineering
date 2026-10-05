using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Queries.GetCurrentUserTemporaryDailies;

public record GetCurrentUserTemporaryDailiesQuery(
                                                 long CreatorId,
                                                 long? CostCenterId,
                                                 long? ProjectId,
                                                 long? ProjectOperationId,
                                                 TemporaryDailyStatus? Status,
                                                 DateTime? StartDate,
                                                 DateTime? EndDate,
                                                 string? FilterData,
                                                 string[]? OrderBy,
                                                 int PageIndex,
                                                 int PageSize) : IQuery<DataResult<List<ProjectOperationTemporaryDaily>>>;
