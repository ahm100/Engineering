using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Queries.GetOperationInfoSeasonById;

public record GetOperationInfoSeasonByIdQuery(
    long Id
    ) : IQuery<OperationInfoSeason>;