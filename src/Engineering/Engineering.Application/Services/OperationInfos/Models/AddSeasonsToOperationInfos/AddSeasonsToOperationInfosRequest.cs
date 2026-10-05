namespace Engineering.Application.Services.OperationInfos.Models.AddSeasonsToOperationInfos;

public record AddSeasonsToOperationInfosRequest(
    List<long> Ids,
    List<long>? SeasonIds
    ) : IHttpRequest;