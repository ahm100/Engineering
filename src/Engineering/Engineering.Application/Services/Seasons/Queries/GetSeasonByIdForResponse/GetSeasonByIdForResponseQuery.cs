using Engineering.Application.Services.Seasons.Models.GetSeasonById;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByIdForResponse;

public record GetSeasonByIdForResponseQuery(
    long Id) : IQuery<GetSeasonByIdResponse?>;