namespace Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroup;

public record GetsOperationInfoGroupRequest(
    string? FilterData,
    bool? IsActive,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
