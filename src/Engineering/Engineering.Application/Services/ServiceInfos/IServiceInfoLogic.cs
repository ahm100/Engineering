using Engineering.Application.Services.ServiceInfos.Models.ActiveService;
using Engineering.Application.Services.ServiceInfos.Models.CodeCreator;
using Engineering.Application.Services.ServiceInfos.Models.CreateService;
using Engineering.Application.Services.ServiceInfos.Models.DeleteServiceInfo;
using Engineering.Application.Services.ServiceInfos.Models.GetActiveServices;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceByCode;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceById;
using Engineering.Application.Services.ServiceInfos.Models.GetServiceByName;
using Engineering.Application.Services.ServiceInfos.Models.GetServices;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByOperationInfo;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoByProjectOperationIds;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelEnum;
using Engineering.Application.Services.ServiceInfos.Models.GetsServiceInfoExcelExporter;
using Engineering.Application.Services.ServiceInfos.Models.InactiveService;
using Engineering.Application.Services.ServiceInfos.Models.ServiceInfoExcelImports;
using Engineering.Application.Services.ServiceInfos.Models.ServiceInfoGroupDelete;
using Engineering.Application.Services.ServiceInfos.Models.SetServiceInfoDetail;
using Engineering.Application.Services.ServiceInfos.Models.StateChangerServiceInfos;
using Engineering.Application.Services.ServiceInfos.Models.UpdateService;

namespace Engineering.Application.Services.ServiceInfos;

public interface IServiceInfoLogic
{
    ///Commands
    Task<Result<CreateServiceInfoResponse?>> CreateServiceInfo(CreateServiceInfoRequest request, CT ct);
    Task<Result<ServiceInfoExcelImportsResponse?>> ServiceInfoExcelImports(ServiceInfoExcelImportsRequest request, CT ct);
    Task<Result<UpdateServiceInfoResponse?>> UpdateServiceInfo(UpdateServiceInfoRequest request, CT ct);
    Task<Result<InactiveServiceInfoResponse?>> InactiveServiceInfo(InactiveServiceInfoRequest request, CT ct);
    Task<Result<ActiveServiceInfoResponse?>> ActiveServiceInfo(ActiveServiceInfoRequest request, CT ct);
    Task<Result<ServiceInfoCodeCreatorResponse?>> CodeCreator(ServiceInfoCodeCreatorRequest request, CT ct);
    Task<Result<StateChangerServiceInfosResponse?>> StateChangerServiceInfos(StateChangerServiceInfosRequest request, CT ct);
    Task<Result<DeleteServiceInfoResponse?>> DeleteServiceInfo(DeleteServiceInfoRequest request, CT ct);
    Task<Result<ServiceInfoGroupDeleteResponse?>> ServiceInfoGroupDelete(ServiceInfoGroupDeleteRequest request, CT ct);

    ///Queries
    Task<Result<GetServiceInfoByIdResponse?>> GetServiceInfoById(GetServiceInfoByIdRequest request, CT ct);
    Task<Result<GetServiceInfoByNameResponse?>> GetServiceInfoByName(GetServiceInfoByNameRequest request, CT ct);
    Task<Result<GetServiceInfoByCodeResponse?>> GetServiceInfoByCode(GetServiceInfoByCodeRequest request, CT ct);
    Task<Result<GetActiveServiceInfosResponse?>> GetActiveServiceInfos(GetActiveServiceInfosRequest request, CT ct);
    Task<Result<GetServiceInfosResponse?>> GetServiceInfos(GetServiceInfosRequest request, CT ct);
    Task<Result<GetsServiceInfoByOperationInfoResponse?>> GetsServiceInfoByOperationInfo(GetsServiceInfoByOperationInfoRequest request, CT ct);
    Task<Result<GetsServiceInfoByProjectOperationIdsResponse?>> GetsServiceInfoByProjectOperationIds(GetsServiceInfoByProjectOperationIdsRequest request, CT ct);
    Task<Result<GetsServiceInfoExcelEnumResponse?>> GetsServiceInfoExcelEnum(GetsServiceInfoExcelEnumRequest request, CT ct);
    Task<Result<GetsServiceInfoExcelExporterResponse?>> GetsServiceInfoExcelExporter(GetsServiceInfoExcelExporterRequest request, CT ct);
    Task<Result<SetServiceInfoDetailResponse?>> SetServiceInfoDetail(SetServiceInfoDetailRequest request, CT ct);
}