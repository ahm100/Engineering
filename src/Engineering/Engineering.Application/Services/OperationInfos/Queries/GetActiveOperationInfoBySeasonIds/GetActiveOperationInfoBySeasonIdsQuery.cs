using Engineering.Domain.Entities.OperationInfos;
namespace Engineering.Application.Services.OperationInfos.Queries.GetActiveOperationInfoBySeasonIds;

public record GetActiveOperationInfoBySeasonIdsQuery(
    List<long>? CategoryId,
    List<long>? BranchId,
    List<long>? SeasonId,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<List<OperationInfo?>?>;
