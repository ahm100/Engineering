using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceFiltered;

public record GetsOperationInfoServiceFilteredQuery(
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    string? FilterData,
    long? CompanyId,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<OperationInfoService>>>;