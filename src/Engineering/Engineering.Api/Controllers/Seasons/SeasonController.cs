using Engineering.Application.Services.Seasons;
using Engineering.Application.Services.Seasons.Models.ActiveSeason;
using Engineering.Application.Services.Seasons.Models.CodeCreator;
using Engineering.Application.Services.Seasons.Models.CreateSeason;
using Engineering.Application.Services.Seasons.Models.DisableSeason;
using Engineering.Application.Services.Seasons.Models.GetActiveSeasons;
using Engineering.Application.Services.Seasons.Models.GetsByBranchId;
using Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;
using Engineering.Application.Services.Seasons.Models.GetSeasonByCode;
using Engineering.Application.Services.Seasons.Models.GetSeasonById;
using Engineering.Application.Services.Seasons.Models.GetSeasonByName;
using Engineering.Application.Services.Seasons.Models.GetSeasons;
using Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;
using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelEnum;
using Engineering.Application.Services.Seasons.Models.GetsSeasonExcelExporter;
using Engineering.Application.Services.Seasons.Models.InactiveSeason;
using Engineering.Application.Services.Seasons.Models.SeasonGroupDelete;
using Engineering.Application.Services.Seasons.Models.StateChangerSeasons;
using Engineering.Application.Services.Seasons.Models.UpdateSeason;

namespace Engineering.Api.Controllers.Seasons;

[ApiController]
[Route("api/engineering/v1/Season")]
public class SeasonController : ControllerBase
{
    private readonly ISeasonLogic _logic;

    public SeasonController(ISeasonLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddSeason")]
    [ResponseSchema<CreateSeasonResponse>]
    public async Task<IResult> AddSeason([FromBody] CreateSeasonRequest request, CT ct)
    {
        var result = await _logic.CreateSeason(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SeasonCodeCreator")]
    [ResponseSchema<SeasonCodeCreatorResponse>]
    public async Task<IResult> SeasonCodeCreator([FromBody] SeasonCodeCreatorRequest request, CT ct)
    {
        var result = await _logic.CodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("SeasonGroupDelete")]
    [ResponseSchema<SeasonGroupDeleteResponse>]
    public async Task<IResult> SeasonGroupDelete([FromBody] SeasonGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.SeasonGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateSeasons")]
    [ResponseSchema<StateChangerSeasonsResponse>]
    public async Task<IResult> ActivateSeasons([FromBody] ActivateSeasonsRequest request, CT ct)
    {
        var result = await _logic.StateChangerSeasons(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateSeasons")]
    [ResponseSchema<StateChangerSeasonsResponse>]
    public async Task<IResult> InactivateSeasons([FromBody] InactivateSeasonsRequest request, CT ct)
    {
        var result = await _logic.StateChangerSeasons(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditSeason")]
    [ResponseSchema<UpdateSeasonResponse>]
    public async Task<IResult> EditSeason([FromBody] UpdateSeasonRequest request, CT ct)
    {
        var result = await _logic.UpdateSeason(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveSeason")]
    [ResponseSchema<ActiveSeasonResponse>]
    public async Task<IResult> ActiveSeason([FromBody] ActiveSeasonRequest request, CT ct)
    {
        var result = await _logic.ActiveSeason(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveSeason")]
    [ResponseSchema<InactiveSeasonResponse>]
    public async Task<IResult> InactiveSeason([FromBody] InactiveSeasonRequest request, CT ct)
    {
        var result = await _logic.InactiveSeason(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetSeasonById")]
    [ResponseSchema<GetSeasonByIdResponse>]
    public async Task<IResult> GetSeasonById([FromQuery] GetSeasonByIdRequest request, CT ct)
    {
        var result = await _logic.GetSeasonById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetSeasonByName")]
    [ResponseSchema<GetSeasonByNameResponse>]
    public async Task<IResult> GetSeasonByName([FromQuery] GetSeasonByNameRequest request, CT ct)
    {
        var result = await _logic.GetSeasonByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetSeasonByCode")]
    [ResponseSchema<GetSeasonByCodeResponse>]
    public async Task<IResult> GetSeasonByCode([FromQuery] GetSeasonByCodeRequest request, CT ct)
    {
        var result = await _logic.GetSeasonByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveSeason")]
    [ResponseSchema<GetActiveSeasonsResponse>]
    public async Task<IResult> GetsActiveSeason([FromQuery] GetActiveSeasonsRequest request, CT ct)
    {
        var result = await _logic.GetActiveSeasons(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsSeason")]
    [ResponseSchema<GetSeasonsResponse>]
    public async Task<IResult> GetsSeason([FromQuery] GetSeasonsRequest request, CT ct)
    {
        var result = await _logic.GetSeasons(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByBranchId")]
    [ResponseSchema<GetsByBranchIdResponse>]
    public async Task<IResult> GetsByBranchId([FromQuery] GetsByBranchIdRequest request, CT ct)
    {
        var result = await _logic.GetsByBranchId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByBranchIdWhithOperationInfo")]
    [ResponseSchema<GetsByBranchIdWhithOperationInfoResponse>]
    public async Task<IResult> GetsByBranchIdWhithOperationInfo([FromQuery] GetsByBranchIdWhithOperationInfoRequest request, CT ct)
    {
        var result = await _logic.GetsByBranchIdWhithOperationInfo(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsSeasonByBranchIds")]
    [ResponseSchema<GetsSeasonByBranchIdsResponse>]
    public async Task<IResult> GetsSeasonByBranchIds([FromBody] GetsSeasonByBranchIdsRequest request, CT ct)
    {
        var result = await _logic.GetsSeasonByBranchIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsSeasonExcelEnum")]
    [ResponseSchema<GetsSeasonExcelEnumResponse>]
    public async Task<IResult> GetsSeasonExcelEnum([FromQuery] GetsSeasonExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsSeasonExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsSeasonExcelExporter")]
    [ResponseSchema<GetsSeasonExcelExporterResponse>]
    public async Task<IResult> GetsSeasonExcelExporter([FromBody] GetsSeasonExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsSeasonExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableSeason")]
    [ResponseSchema<DisableSeasonResponse>]
    public async Task<IResult> DisableSeason([FromQuery] DisableSeasonRequest request, CT ct)
    {
        var result = await _logic.DisableSeason(request, ct);
        return result.GetHttpResponse();
    }
}