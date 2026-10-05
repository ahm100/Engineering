namespace Engineering.Application.Services.OperationInfos.Models.GetsBySeasonId;

public record GetsBySeasonIdRequest(
    long SeasonId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
