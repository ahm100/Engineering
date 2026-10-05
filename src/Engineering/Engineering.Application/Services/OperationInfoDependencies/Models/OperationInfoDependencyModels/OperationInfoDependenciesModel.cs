namespace Engineering.Application.Services.OperationInfoDependencies.Models.OperationInfoDependencyModels;

public record OperationInfoDependenciesModel(
    long OperationInfoId,
    string OperationInfoCode,
    string OperationInfoName,
    List<DependenciesModel>? DependenciesModel
    );
