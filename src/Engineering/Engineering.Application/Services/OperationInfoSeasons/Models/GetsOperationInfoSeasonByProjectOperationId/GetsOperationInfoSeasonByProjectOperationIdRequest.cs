namespace Engineering.Application.Services.OperationInfoSeasons.Models.GetsOperationInfoSeasonByProjectOperationId;

public record GetsOperationInfoSeasonByProjectOperationIdRequest(
    long ProjectOperationId
     ) : IHttpRequest;
