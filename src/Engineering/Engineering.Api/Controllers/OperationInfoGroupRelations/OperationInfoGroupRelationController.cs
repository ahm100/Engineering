using Engineering.Application.Services.OperationInfoGroupRelations;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.CreateOperationInfoGroupRelation;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoGroupId;
using Engineering.Application.Services.OperationInfoGroupRelations.Models.GetsByOperationInfoId;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/OperationInfoGroupRelation")]
public class OperationInfoGroupRelationController : ControllerBase
{
    private readonly IOperationInfoGroupRelationLogic _logic;

    public OperationInfoGroupRelationController(IOperationInfoGroupRelationLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddOperationInfoGroupRelation")]
    [ResponseSchema<CreateOperationInfoGroupRelationResponse>]
    public async Task<IResult> AddOperationInfoGroupRelation(
    [FromBody] CreateOperationInfoGroupRelationRequest request,
    CT ct)
    {
        var result = await _logic.CreateOperationInfoGroupRelation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByOperationInfoId")]
    [ResponseSchema<GetsOperationInfoGroupRelationByIdResponse>]
    public async Task<IResult> GetsByOperationInfoId(
        [FromQuery] GetsOperationInfoGroupRelationByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsByOperationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByOperationInfoGroupId")]
    [ResponseSchema<GetsOperationInfoGroupRelationByGroupIdResponse>]
    public async Task<IResult> GetsByOperationInfoGroupId(
        [FromQuery] GetsOperationInfoGroupRelationByGroupIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsByOperationInfoGroupId(request, ct);
        return result.GetHttpResponse();
    }
}