using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.OperationInfoDependency;

public record OperationInfoDependencyRequestModel(
    long RelationId,
    int WorkingDays,
    OperationInfoDependencyType DependencyType
    );
