using Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencies;
using Engineering.Application.Services.OperationInfoDependencies.Models.GetOperationInfoDependencyById;
using Engineering.Application.Services.OperationInfoDependencies.Models.GetsOperationInfoDependencyType;
using Engineering.Application.Services.OperationInfoDependencies.Models.UpdateOperationInfoDependency;

namespace Engineering.Application.Services.OperationInfoDependencies;

public interface IOperationInfoDependencyLogic
{
    ///Commands
    Task<Result<UpdateOperationInfoDependencyResponse?>> UpdateOperationInfoDependency(
        UpdateOperationInfoDependencyRequest request, CT ct);

    ///Queries
    Task<Result<GetOperationInfoDependencyByIdResponse?>> GetOperationInfoDependencyById(
        GetOperationInfoDependencyByIdRequest request, CT ct);

    Task<Result<GetOperationInfoDependenciesResponse?>> GetOperationInfoDependencies(
        GetOperationInfoDependenciesRequest request, CT ct);

    Task<Result<GetsOperationInfoDependencyTypeResponse?>> GetsOperationInfoDependencyType(
        GetsOperationInfoDependencyTypeRequest request, CT ct);
}