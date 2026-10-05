using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencies;

public record GetOperationInfoDependenciesRequest(
    long OperationInfoId,
    OperationInfoDependencyType? DependencyType,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
