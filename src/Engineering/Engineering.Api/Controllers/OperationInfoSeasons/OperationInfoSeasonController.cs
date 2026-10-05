using Engineering.Application.Services.OperationInfoSeasons;
using Engineering.Application.Services.OperationInfoSeasons.Models.CreateOperationInfoSeason;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsByOperationInfoId;
using Engineering.Application.Services.OperationInfoSeasons.Models.GetsOperationInfoSeasonByProjectOperationId;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/OperationInfoSeason")]
public class OperationInfoSeasonController : ControllerBase
{
    private readonly IOperationInfoSeasonLogic _logic;

    public OperationInfoSeasonController(IOperationInfoSeasonLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddOperationInfoSeason")]
    [ResponseSchema<CreateOperationInfoSeasonResponse>]
    public async Task<IResult> AddOperationInfoSeason(
    [FromBody] CreateOperationInfoSeasonRequest request,
    CT ct)
    {
        var newRequest = new CreateOperationInfoSeasonModelRequest(request.OperationInfoIds, null, request.SeasonIds, request.DeletedOperationInfoSeasonIds);
        var result = await _logic.CreateOperationInfoSeason(newRequest, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoSeason")]
    [ResponseSchema<GetsOperationInfoSeasonByIdResponse>]
    public async Task<IResult> GetsOperationInfoSeason(
        [FromQuery] GetsOperationInfoSeasonByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsByOperationInfoId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsOperationInfoSeasonByProjectOperationId")]
    [ResponseSchema<GetsOperationInfoSeasonByProjectOperationIdResponse>]
    public async Task<IResult> GetsOperationInfoSeasonByProjectOperationId(
        [FromQuery] GetsOperationInfoSeasonByProjectOperationIdRequest request,
        CT ct)
    {
        var result = await _logic.GetsOperationInfoSeasonByProjectOperationId(request, ct);
        return result.GetHttpResponse();
    }
}