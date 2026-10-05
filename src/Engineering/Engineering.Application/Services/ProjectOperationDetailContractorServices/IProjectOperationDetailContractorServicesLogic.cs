using Engineering.Application.Services.DetailContractorServices.Models.ActiveDetailContractorService;
using Engineering.Application.Services.DetailContractorServices.Models.InactiveDetailContractorService;
using Engineering.Application.Services.DetailContractorServices.Models.StateChangerDetailContractorServices;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.AppointmentContractor;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.CreateContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.DisableContractorService;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetContractorServiceById;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetFilteredProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByFilter;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsProjectOperationDetailContractors;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsServiceByProjectOperationDetailIds;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Create;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Delete;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetAssignable;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.GetByPO;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.OpAssign.Update;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.SetContractorServiceToContract;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.SetDetailContractorServiceToNew;
using Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.UpdateContractorService;

namespace Engineering.Application.Services.ContractorServices;

public interface IProjectOperationDetailContractorServicesLogic
{
    ///Commands
    Task<Result<CreateContractorServiceResponse?>> CreateContractorService(CreateContractorServiceRequest request, CT ct);
    Task<Result<AppointmentContractorResponse?>> AppointmentContractor(AppointmentContractorRequest request, CT ct);
    Task<Result<UpdateContractorServiceResponse?>> UpdateContractorService(UpdateContractorServiceRequest request, CT ct);
    Task<Result<DisableContractorServiceResponse?>> DisableContractorService(DisableContractorServiceRequest request, CT ct);
    Task<Result<InactiveDetailContractorServiceResponse?>> InactiveDetailContractorService(InactiveDetailContractorServiceRequest request, CT ct);
    Task<Result<ActiveDetailContractorServiceResponse?>> ActiveDetailContractorService(ActiveDetailContractorServiceRequest request, CT ct);
    Task<Result<StateChangerDetailContractorServicesResponse?>> StateChangerDetailContractorServices(StateChangerDetailContractorServicesRequest request, CT ct);
    Task<Result<SetContractorServiceToContractResponse?>> SetContractorServiceToContract(SetContractorServiceToContractRequest request, CT ct);
    Task<Result<SetDetailContractorServiceToNewResponse?>> SetDetailContractorServiceToNew(SetDetailContractorServiceToNewRequest request, CT ct);
    Task<Result<CreateOpAssignResponse?>> CreateOpAssign(CreateOpAssignRequest request, CT ct);
    Task<Result<DeleteOpAssignResponse?>> DeleteOpAssign(DeleteOpAssignRequest request, CT ct);

    ///Queries
    Task<Result<GetContractorServiceByIdResponse?>> GetContractorServiceById(GetContractorServiceByIdRequest request, CT ct);
    Task<Result<GetsContractorServiceByFilterResponse?>> GetsContractorServiceByFilter(GetsContractorServiceByFilterRequest request, CT ct);
    Task<Result<GetsContractorServiceByProjectOperationDetailIdResponse?>> GetsContractorServiceByProjectOperationDetailId(GetsContractorServiceByProjectOperationDetailIdRequest request, CT ct);
    Task<Result<GetsProjectOperationDetailContractorsResponse?>> GetsProjectOperationDetailContractors(GetsProjectOperationDetailContractorsRequest request, CT ct);
    Task<Result<GetsServiceByProjectOperationDetailIdsResponse?>> GetsServiceByProjectOperationDetailIds(GetsServiceByProjectOperationDetailIdsRequest request, CT ct);
    Task<Result<GetFilteredProjectOperationDetailContractorsResponse?>> GetFilteredProjectOperationDetailContractors(GetFilteredProjectOperationDetailContractorsRequest request, CT ct);
    Task<Result<GetOpAssignByPOResponse?>> GetOpAssignByPO(GetOpAssignByPORequest request, CT ct);
    Task<Result<GetAssignablePODsResponse?>> GetAssignablePODs(GetAssignablePODsRequest request, CT ct);
    Task<Result<UpdateOpAssignResponse?>> UpdateOpAssign(UpdateOpAssignRequest request, CT ct);
}
