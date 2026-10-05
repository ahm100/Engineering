using Engineering.Application.Services.ProjectOperationDetailContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.CreatePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.DeletePODContractorExperts;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCServiceId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.GetPODContractorExpertsByCVEId;
using Engineering.Application.Services.ProjectOperationDetailContractorExperts.Contracts.UpdatePODContractorExperts;

namespace Engineering.Api.Controllers.ProjectOperationDetailContractorExperts;

[ApiController]
[Route("api/engineering/v1/ProjectOperationDetailContractorExpert")]
public class ProjectOperationDetailContractorExpertController : ControllerBase
{
    private readonly IProjectOperationDetailContractorExpertLogic _logic;

    public ProjectOperationDetailContractorExpertController(IProjectOperationDetailContractorExpertLogic logic)
    {
        _logic = logic;
    }


    [HttpPost("CreatePODContractorExperts")]
    [ResponseSchema<CreatePODContractorExpertsResponse>]
    public async Task<IResult> CreatePODContractorExperts(
    [FromBody] CreatePODContractorExpertsRequest request,
    CT ct)
    {
        var result = await _logic.CreatePODContractorExperts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdatePODContractorExperts")]
    [ResponseSchema<UpdatePODContractorExpertsResponse>]
    public async Task<IResult> UpdatePODContractorExperts(
    [FromBody] UpdatePODContractorExpertsRequest request,
    CT ct)
    {
        var result = await _logic.UpdatePODContractorExperts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeletePODContractorExperts")]
    [ResponseSchema<DeletePODContractorExpertsResponse>]
    public async Task<IResult> DeletePODContractorExperts(
    [FromQuery] DeletePODContractorExpertsRequest request,
    CT ct)
    {
        var result = await _logic.DeletePODContractorExperts(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPODContractorExpertsByCServiceId")]
    [ResponseSchema<GetPODContractorExpertsByCServiceIdResponse>]
    public async Task<IResult> GetPODContractorExpertsByCServiceId(
    [FromQuery] GetPODContractorExpertsByCServiceIdRequest request,
    CT ct)
    {
        var result = await _logic.GetPODContractorExpertsByCServiceId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetPODContractorExpertsByCVEId")]
    [ResponseSchema<GetPODContractorExpertsByCVEIdResponse>]
    public async Task<IResult> GetPODContractorExpertsByCVEId(
    [FromQuery] GetPODContractorExpertsByCVEIdRequest request,
    CT ct)
    {
        var result = await _logic.GetPODContractorExpertsByCVEId(request, ct);
        return result.GetHttpResponse();
    }
}