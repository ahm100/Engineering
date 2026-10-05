using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetByNames;

public record GetByNamesQuery
(
    List<string> Names
) : IQuery<List<Season>>;
