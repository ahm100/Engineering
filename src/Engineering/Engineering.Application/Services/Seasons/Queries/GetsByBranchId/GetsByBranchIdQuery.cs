
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetsByBranchId;

public record GetsByBranchIdQuery(
    long BranchId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<Season>>>;