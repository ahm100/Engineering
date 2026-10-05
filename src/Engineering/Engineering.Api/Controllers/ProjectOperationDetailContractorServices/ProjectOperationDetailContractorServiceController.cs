using Engineering.Application.Services.ContractorServices;
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

namespace Engineering.Api.Controllers.ProjectOperationDetailContractorServices;

[ApiController]
[Route("api/engineering/v1/ProjectOperationDetailContractorService")]
[Tags("ProjectOperationDetailContractorService")]
public class ContractorServiceController : ControllerBase
{
    private readonly IProjectOperationDetailContractorServicesLogic _logic;

    public ContractorServiceController(IProjectOperationDetailContractorServicesLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddContractorService")]
    [ResponseSchema<CreateContractorServiceResponse>]
    public async Task<IResult> AddContractorService(
    [FromBody] CreateContractorServiceRequest request,
    CT ct)
    {
        var result = await _logic.CreateContractorService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("AppointmentContractor")]
    [ResponseSchema<AppointmentContractorResponse>]
    public async Task<IResult> AppointmentContractor(
        [FromBody] AppointmentContractorRequest request,
        CT ct)
    {
        var result = await _logic.AppointmentContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditContractorService")]
    [ResponseSchema<UpdateContractorServiceResponse>]
    public async Task<IResult> EditContractorService(
        [FromBody] UpdateContractorServiceRequest request,
        CT ct)
    {
        var result = await _logic.UpdateContractorService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateDetailContractorServices")]
    [ResponseSchema<StateChangerDetailContractorServicesResponse>]
    public async Task<IResult> ActivateDetailContractorServices(
        [FromBody] ActivateDetailContractorServicesRequest request,
        CT ct)
    {
        var result = await _logic.StateChangerDetailContractorServices(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateDetailContractorServices")]
    [ResponseSchema<StateChangerDetailContractorServicesResponse>]
    public async Task<IResult> InactivateDetailContractorServices(
        [FromBody] InactivateDetailContractorServicesRequest request,
        CT ct)
    {
        var result = await _logic.StateChangerDetailContractorServices(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveDetailContractorService")]
    [ResponseSchema<ActiveDetailContractorServiceResponse>]
    public async Task<IResult> ActiveDetailContractorService(
        [FromBody] ActiveDetailContractorServiceRequest request,
        CT ct)
    {
        var result = await _logic.ActiveDetailContractorService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveDetailContractorService")]
    [ResponseSchema<InactiveDetailContractorServiceResponse>]
    public async Task<IResult> InactiveDetailContractorService(
        [FromBody] InactiveDetailContractorServiceRequest request,
        CT ct)
    {
        var result = await _logic.InactiveDetailContractorService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetContractorServiceToContract")]
    [ResponseSchema<SetContractorServiceToContractResponse>]
    public async Task<IResult> SetContractorServiceToContract(
        [FromBody] SetContractorServiceToContractRequest request,
        CT ct)
    {
        var result = await _logic.SetContractorServiceToContract(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("SetDetailContractorServiceToNew")]
    [ResponseSchema<SetDetailContractorServiceToNewResponse>]
    public async Task<IResult> SetDetailContractorServiceToNew(
        [FromBody] SetDetailContractorServiceToNewRequest request,
        CT ct)
    {
        var result = await _logic.SetDetailContractorServiceToNew(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorServiceById")]
    [ResponseSchema<GetContractorServiceByIdResponse>]
    public async Task<IResult> GetContractorServiceById(
        [FromQuery] GetContractorServiceByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetContractorServiceById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorServiceByFilter")]
    [ResponseSchema<GetsContractorServiceByFilterResponse>]
    public async Task<IResult> GetsContractorServiceByFilter(
        [FromBody] GetsContractorServiceByFilterRequest request,
        CT ct)
    {
        var result = await _logic.GetsContractorServiceByFilter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsContractorServiceByProjectOperationDetailId")]
    [ResponseSchema<GetsContractorServiceByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetsContractorServiceByProjectOperationDetailId(
        [FromQuery] GetsContractorServiceByProjectOperationDetailIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsContractorServiceByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsProjectOperationDetailContractors")]
    [ResponseSchema<GetsProjectOperationDetailContractorsResponse>]
    public async Task<IResult> GetsProjectOperationDetailContractors(
        [FromQuery] GetsProjectOperationDetailContractorsRequest request,
        CT ct)
    {
        var result = await _logic.GetsProjectOperationDetailContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredProjectOperationDetailContractors")]
    [ResponseSchema<GetFilteredProjectOperationDetailContractorsResponse>]
    public async Task<IResult> GetFilteredProjectOperationDetailContractors(
        [FromBody] GetFilteredProjectOperationDetailContractorsRequest request,
        CT ct)
    {
        var result = await _logic.GetFilteredProjectOperationDetailContractors(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsServiceByProjectOperationDetailIds")]
    [ResponseSchema<GetsServiceByProjectOperationDetailIdsResponse>]
    public async Task<IResult> GetsServiceByProjectOperationDetailIds(
        [FromBody] GetsServiceByProjectOperationDetailIdsRequest request,
        CT ct)
    {
        var result = await _logic.GetsServiceByProjectOperationDetailIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteContractorService")]
    [ResponseSchema<DisableContractorServiceResponse>]
    public async Task<IResult> DeleteContractorService(
        [FromBody] DisableContractorServiceRequest request,
        CT ct)
    {
        var result = await _logic.DisableContractorService(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsOperationBasedPODsByPOId")]
    [ResponseSchema<GetOpAssignByPOResponse>]
    public async Task<IResult> GetsOperationBasedAssignmentsByProjectOperationId(
    [FromBody] GetOpAssignByPORequest request,
    CT ct)
    {
        var result =
            await _logic.GetOpAssignByPO(request, ct);

        return result.GetHttpResponse();
    }

    [HttpPost("GetOpAssignByPO")]
    [ResponseSchema<GetOpAssignByPOResponse>]
    public async Task<IResult> GetOpAssignByPO(
    [FromBody] GetOpAssignByPORequest request,
    CT ct)
    {
        var result = await _logic.GetOpAssignByPO(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetAssignablePODs")]
    [ResponseSchema<GetAssignablePODsResponse>]
    public async Task<IResult> GetAssignablePODs(
        [FromBody] GetAssignablePODsRequest request,
        CT ct)
    {
        var result = await _logic.GetAssignablePODs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateOpAssign")]
    [ResponseSchema<CreateOpAssignResponse>]
    public async Task<IResult> CreateOpAssign(
        [FromBody] CreateOpAssignRequest request,
        CT ct)
    {
        var result = await _logic.CreateOpAssign(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateOpAssign")]
    [ResponseSchema<UpdateOpAssignResponse>]
    public async Task<IResult> UpdateOpAssign(
        [FromBody] UpdateOpAssignRequest request,
        CT ct)
    {
        var result = await _logic.UpdateOpAssign(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteOpAssign")]
    [ResponseSchema<DeleteOpAssignResponse>]
    public async Task<IResult> DeleteOpAssign(
        [FromBody] DeleteOpAssignRequest request,
        CT ct)
    {
        var result = await _logic.DeleteOpAssign(request, ct);
        return result.GetHttpResponse();
    }
}