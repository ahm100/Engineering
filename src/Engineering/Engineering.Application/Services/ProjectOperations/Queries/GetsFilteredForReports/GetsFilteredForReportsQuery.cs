using Engineering.Application.Services.ProjectOperations.Models.GetsFilteredForReports;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsFilteredForReports;

public record GetsFilteredForReportsQuery(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsFilteredForReportsModel>>>;