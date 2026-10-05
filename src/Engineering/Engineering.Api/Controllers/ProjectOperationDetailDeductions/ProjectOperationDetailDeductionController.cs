using Engineering.Application.Services.ProjectOperationDetailDeductions;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.CreateProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.DeleteProjectOperationDetailDeduction;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetDeductionAmountByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetProjectOperationDetailDeductionById;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetsDeductionByProjectOperationDetailId;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.ProjectOperationDetailDeductionGroupDelete;
using Engineering.Application.Services.ProjectOperationDetailDeductions.Models.UpdateProjectOperationDetailDeduction;

namespace Engineering.Api.Controllers.ProjectOperationDetailDeductions;

[ApiController]
[Route("api/engineering/v1/ProjectOperationDetailDeduction")]
public class ProjectOperationDetailDeductionController : ControllerBase
{
    private readonly IProjectOperationDetailDeductionLogic _logic;

    public ProjectOperationDetailDeductionController(IProjectOperationDetailDeductionLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateDeduction")]
    [ResponseSchema<CreateProjectOperationDetailDeductionResponse>]
    public async Task<IResult> CreateDeduction([FromBody] CreateProjectOperationDetailDeductionRequest request, CT ct)
    {
        var result = await _logic.CreateProjectOperationDetailDeduction(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateDeduction")]
    [ResponseSchema<UpdateProjectOperationDetailDeductionResponse>]
    public async Task<IResult> UpdateDeduction([FromBody] UpdateProjectOperationDetailDeductionRequest request, CT ct)
    {
        var result = await _logic.UpdateProjectOperationDetailDeduction(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetDeductionById")]
    [ResponseSchema<GetProjectOperationDetailDeductionByIdResponse>]
    public async Task<IResult> GetDeductionById([FromQuery] GetProjectOperationDetailDeductionByIdRequest request, CT ct)
    {
        var result = await _logic.GetProjectOperationDetailDeductionById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTotalAmountByProjectOperationDetailId")]
    [ResponseSchema<GetDeductionAmountByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetTotalAmountByProjectOperationDetailId([FromQuery] GetDeductionAmountByProjectOperationDetailIdRequest request, CT ct)
    {
        var result = await _logic.GetDeductionAmountByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsDeductionByProjectOperationDetailId")]
    [ResponseSchema<GetsDeductionByProjectOperationDetailIdResponse>]
    public async Task<IResult> GetsDeductionByProjectOperationDetailId([FromBody] GetsDeductionByProjectOperationDetailIdRequest request, CT ct)
    {
        var result = await _logic.GetsDeductionByProjectOperationDetailId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteDeduction")]
    [ResponseSchema<DeleteProjectOperationDetailDeductionResponse>]
    public async Task<IResult> DeleteDeduction([FromBody] DeleteProjectOperationDetailDeductionRequest request, CT ct)
    {
        var result = await _logic.DeleteProjectOperationDetailDeduction(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeductionGroupDelete")]
    [ResponseSchema<ProjectOperationDetailDeductionGroupDeleteResponse>]
    public async Task<IResult> DeductionGroupDelete([FromBody] ProjectOperationDetailDeductionGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.ProjectOperationDetailDeductionGroupDelete(request, ct);
        return result.GetHttpResponse();
    }
}