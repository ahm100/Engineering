using Engineering.Application.Services.Seasons.Models.SeasonModels;

namespace Engineering.Application.Services.Seasons.Queries.GetsByBranchIdForResponse;

public record GetsByBranchIdForResponseQuery(
    long BranchId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsByBranchIdModel>>?>;