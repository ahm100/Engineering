namespace Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceFiltered;

public record GetsOperationInfoServiceFilteredRequest(
    long? CategoryId,
    long? BranchId,
    long? SeasonId,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
