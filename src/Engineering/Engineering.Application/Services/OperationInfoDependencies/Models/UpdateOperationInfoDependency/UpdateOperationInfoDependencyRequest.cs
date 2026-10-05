using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfoDependencies.Models.UpdateOperationInfoDependency;

public record UpdateOperationInfoDependencyRequest(
    long Id,
    long RelationId,
    int WorkingDays,
    OperationInfoDependencyType DependencyType
     ) : IHttpRequest;
