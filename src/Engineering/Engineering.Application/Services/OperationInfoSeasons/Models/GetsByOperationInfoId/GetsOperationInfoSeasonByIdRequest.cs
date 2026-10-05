namespace Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;

public record GetsOperationInfoSeasonByIdRequest(
    long OprationInfoId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
