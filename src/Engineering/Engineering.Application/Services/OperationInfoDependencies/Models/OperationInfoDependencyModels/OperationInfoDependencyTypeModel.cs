using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfoDependencies.Models.OperationInfoDependencyModels;

public record OperationInfoDependencyTypeModel(
    int TypeId,
    OperationInfoDependencyType DependencyType,
    string TypeTitle
    );
