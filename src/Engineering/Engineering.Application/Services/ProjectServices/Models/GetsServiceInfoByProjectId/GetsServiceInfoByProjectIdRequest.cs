namespace Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;

public record GetsServiceInfoByProjectIdRequest(
        long ProjectId,
        string? FilterData,
        int PageIndex,
        int PageSize
     ) : IHttpRequest;
