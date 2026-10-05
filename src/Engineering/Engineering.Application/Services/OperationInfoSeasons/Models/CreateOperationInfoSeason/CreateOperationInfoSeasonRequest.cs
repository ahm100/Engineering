using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeason;

public record CreateOperationInfoSeasonRequest(
    List<long> OperationInfoIds,
    List<long>? SeasonIds,
    List<long>? DeletedOperationInfoSeasonIds
     ) : IHttpRequest;

public record CreateOperationInfoSeasonModelRequest(
    List<long>? OperationInfoIds,
    List<OperationInfo>? OperationInfos,
    List<long>? SeasonIds,
    List<long>? DeletedOperationInfoSeasonIds
     ) : IHttpRequest;
