using Engineering.Application.Services.GoodsManagerAssignments;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.CreateGoodsManagerAssignment;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.DeleteGoodsManagerAssignment;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentHistory;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentsById;
using Engineering.Application.Services.GoodsManagerAssignments.Contracts.UpdateGoodsManagerAssignment;

namespace Engineering.Api.Controllers.GoodsManagerAssignments;

[ApiController]
[Route("api/engineering/v1/GoodsManagerAssignment")]
public class GoodsManagerAssignmentController : ControllerBase
{
    private readonly IGoodsManagerAssignmentLogic _logic;

    public GoodsManagerAssignmentController(
        IGoodsManagerAssignmentLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateGoodsManagerAssignment")]
    [ResponseSchema<CreateGoodsManagerAssignmentResponse>]
    public async Task<IResult> CreateGoodsManagerAssignment(
        [FromBody] CreateGoodsManagerAssignmentRequest request, CT ct)
    {
        var result = await _logic.CreateGoodsManagerAssignment(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFilteredGoodsManagerAssignments")]
    [ResponseSchema<GetFilteredGoodsManagerAssignmentsResponse>]
    public async Task<IResult> GetFilteredGoodsManagerAssignments(
        [FromBody] GetFilteredGoodsManagerAssignmentsRequest request, CT ct)
    {
        var result = await _logic.GetFilteredGoodsManagerAssignments(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetGoodsManagerAssignmentsById")]
    [ResponseSchema<GetGoodsManagerAssignmentsByIdResponse>]
    public async Task<IResult> GetGoodsManagerAssignmentsById(
        [FromBody] GetGoodsManagerAssignmentsByIdRequest request, CT ct)
    {
        var result = await _logic.GetGoodsManagerAssignmentsById(request, ct);
        return result.GetHttpResponse();
    }


    [HttpDelete("DeleteGoodsManagerAssignment")]
    [ResponseSchema<DeleteGoodsManagerAssignmentResponse>]
    public async Task<IResult> DeleteGoodsManagerAssignment(
    [FromBody] DeleteGoodsManagerAssignmentRequest request,
    CT ct)
    {
        var result = await _logic.DeleteGoodsManagerAssignment(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateGoodsManagerAssignment")]
    [ResponseSchema<UpdateGoodsManagerAssignmentResponse>]
    public async Task<IResult> UpdateGoodsManagerAssignment(
        [FromBody] UpdateGoodsManagerAssignmentRequest request, CT ct)
    {
        var result = await _logic.UpdateGoodsManagerAssignment(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetGoodsManagerAssignmentHistory")]
    [ResponseSchema<GetGoodsManagerAssignmentHistoryResponse>]
    public async Task<IResult> GetGoodsManagerAssignmentHistory(
        [FromBody] GetGoodsManagerAssignmentHistoryRequest request, CT ct)
    {
        var result = await _logic.GetGoodsManagerAssignmentHistory(request, ct);
        return result.GetHttpResponse();
    }
}