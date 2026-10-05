using Engineering.Application.Services.OperationInfoDependencies.Models.OperationInfoDependencyModels;

namespace Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencies;

public record GetOperationInfoDependenciesResponse(
    OperationInfoDependenciesModel Data,
    int RowCount);
