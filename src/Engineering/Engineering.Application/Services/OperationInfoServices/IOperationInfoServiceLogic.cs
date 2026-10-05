using Engineering.Application.Services.OperationInfoServices.Models.CreateOperationInfoService;
using Engineering.Application.Services.OperationInfoServices.Models.DeleteOperationInfoService;
using Engineering.Application.Services.OperationInfoServices.Models.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;
using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceFiltered;

namespace Engineering.Application.Services.OperationInfoServices;

public interface IOperationInfoServiceLogic
{
    Task<Result<CreateOperationInfoServiceResponse?>> CreateOperationInfoService(
        CreateOperationInfoServiceModelRequest request, CT ct);

    Task<Result<DeleteOperationInfoServiceResponse?>> DeleteOperationInfoService(
        DeleteOperationInfoServiceRequest request, CT ct);

    Task<Result<GetsByOperationInfoIdResponse?>> GetsByOperationInfoId(
        GetsByOperationInfoIdRequest request, CT ct);

    Task<Result<GetsOperationInfoServiceFilteredResponse?>> GetsOperationInfoServiceFiltered(
        GetsOperationInfoServiceFilteredRequest request, CT ct);

    Task<Result<GetsOperationInfoServiceByProjectIdResponse?>> GetsOperationInfoServiceByProjectId(
        GetsOperationInfoServiceByProjectIdRequest request, CT ct);
}