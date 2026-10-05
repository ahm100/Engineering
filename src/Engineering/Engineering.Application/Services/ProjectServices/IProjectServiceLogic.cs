using Engineering.Application.Services.ProjectServices.Models.ActiveProjectService;
using Engineering.Application.Services.ProjectServices.Models.CreateProjectService;
using Engineering.Application.Services.ProjectServices.Models.DeleteProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetActiveProjectServices;
using Engineering.Application.Services.ProjectServices.Models.GetProjectServiceById;
using Engineering.Application.Services.ProjectServices.Models.GetsContractorProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectService;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectServiceDetail;
using Engineering.Application.Services.ProjectServices.Models.GetsServiceInfoByProjectId;
using Engineering.Application.Services.ProjectServices.Models.InactiveProjectService;
using Engineering.Application.Services.ProjectServices.Models.StateChangerProjectServices;
using Engineering.Application.Services.ProjectServices.Models.UpdateProjectService;

namespace Engineering.Application.Services.ProjectServices;

public interface IProjectServiceLogic
{
    ///Commands
    Task<Result<CreateProjectServiceResponse?>> CreateProjectService(CreateProjectServiceRequest request, CT ct);
    Task<Result<UpdateProjectServiceResponse?>> UpdateProjectService(UpdateProjectServiceRequest request, CT ct);
    Task<Result<InactiveProjectServiceResponse?>> InactiveProjectService(InactiveProjectServiceRequest request, CT ct);
    Task<Result<ActiveProjectServiceResponse?>> ActiveProjectService(ActiveProjectServiceRequest request, CT ct);
    Task<Result<StateChangerProjectServicesResponse?>> StateChangerProjectServices(StateChangerProjectServicesRequest request, CT ct);
    Task<Result<DeleteProjectServiceResponse?>> DeleteProjectService(DeleteProjectServiceRequest request, CT ct);

    ///Queries
    Task<Result<GetProjectServiceByIdResponse?>> GetProjectServiceById(GetProjectServiceByIdRequest request, CT ct);
    Task<Result<GetsProjectServiceResponse?>> GetsProjectService(GetsProjectServiceRequest request, CT ct);
    Task<Result<GetsProjectServiceDetailResponse?>> GetsProjectServiceDetail(GetsProjectServiceDetailRequest request, CT ct);
    Task<Result<GetActiveProjectServicesResponse?>> GetActiveProjectServices(GetActiveProjectServicesRequest request, CT ct);
    Task<Result<GetsServiceInfoByProjectIdResponse?>> GetsServiceInfoByProjectId(GetsServiceInfoByProjectIdRequest request, CT ct);
    Task<Result<GetsContractorProjectServiceResponse?>> GetsContractorProjectService(GetsContractorProjectServiceRequest request, CT ct);
}