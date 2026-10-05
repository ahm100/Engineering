namespace Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;

public record GetsOperationInfoServiceByProjectIdRequest(
    long ProjectId,
    long? ExcludedServiceInfoId,
    string? ServiceInfoFilters,
    string? OperationInfoFilters,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
