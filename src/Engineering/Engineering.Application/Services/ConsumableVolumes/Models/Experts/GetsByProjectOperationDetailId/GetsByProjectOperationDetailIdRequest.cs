namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.GetsByProjectOperationDetailId;

public record GetExpertsByProjectOperationDetailIdRequest(
    long ProjectOperationDetailId,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
