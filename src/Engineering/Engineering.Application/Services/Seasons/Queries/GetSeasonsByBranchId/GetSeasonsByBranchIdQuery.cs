using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonsByBranchId;

public record GetSeasonsByBranchIdQuery(
    long BranchId,
    long CompanyId
    ) : IQuery<List<Season>?>;