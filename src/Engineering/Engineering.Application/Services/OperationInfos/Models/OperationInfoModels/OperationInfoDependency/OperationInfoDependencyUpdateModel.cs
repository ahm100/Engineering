using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.OperationInfos.Enums;

namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.OperationInfoDependency;

public record OperationInfoDependencyUpdateModel(
    OperationInfo DependencyOperationInfo,
    int WorkingDays,
    OperationInfoDependencyType DependencyType
    );
