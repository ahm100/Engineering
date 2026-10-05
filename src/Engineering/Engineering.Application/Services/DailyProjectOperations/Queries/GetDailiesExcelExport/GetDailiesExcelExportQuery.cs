using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailiesExcelExport;

public record GetDailiesExcelExportQuery(
                                         List<long>? Ids,
                                         long ProjectOperationDetailId,
                                         DateTime? StartDate,
                                         DateTime? EndDate,
                                         long? CreatorId,
                                         string? FilterData,
                                         string[]? OrderBy,
                                         int PageIndex,
                                         int PageSize) : IQuery<DataResult<List<DailyProjectOperation>>>;
