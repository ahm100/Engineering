namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetExpertsByProjectOperationId;

public record GetExpertsByProjectOperationIdRequest(
    long ProjectOperationId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
