using Engineering.Application.Services.Adjustments;
using Engineering.Application.Services.Adjustments.Contracts;
using Engineering.Application.Services.Adjustments.Contracts.CreateAdjustmentIndex;
using Engineering.Application.Services.Adjustments.Contracts.DeleteAdjustmentIndex;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexById;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexes;
using Engineering.Application.Services.Adjustments.Contracts.UpdateAdjustmentIndex;
using System.ComponentModel;

namespace Engineering.Api.Controllers.Adjustments;

[Authorize]
[Route("api/engineering/v1/[controller]")]
public class AdjustmentController : ControllerBase
{
    private readonly IAdjustmentLogic _logic;

    public AdjustmentController(IAdjustmentLogic adjustmentLogic)
    {
        _logic = adjustmentLogic;
    }

    [HttpPost("AdjustmentExcelImport")]
    [Description("Import Adjustment Excel")]
    public async Task<IResult> AdjustmentExcelImport(AdjustmentExcelImportsRequest request, CT ct)
    {
        var result = await _logic.AdjustmentExcelImports(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateAdjustmentIndex")]
    [ResponseSchema<CreateAdjustmentIndexResponse>]
    public async Task<IResult> CreateAdjustmentIndex([FromBody] CreateAdjustmentIndexRequest request, CT ct)
    {
        var result = await _logic.CreateAdjustmentIndex(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateAdjustmentIndex")]
    [ResponseSchema<UpdateAdjustmentIndexResponse>]
    public async Task<IResult> UpdateAdjustmentIndex(
        [FromBody] UpdateAdjustmentIndexRequest request, CT ct)
    {
        var result = await _logic.UpdateAdjustmentIndex(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteAdjustmentIndex")]
    [ResponseSchema<DeleteAdjustmentIndexResponse>]
    public async Task<IResult> DeleteAdjustmentIndex([FromQuery] DeleteAdjustmentIndexRequest request, CT ct)
    {
        var result =
            await _logic.DeleteAdjustmentIndex(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetAdjustmentIndexById")]
    [ResponseSchema<GetAdjustmentIndexByIdResponse>]
    public async Task<IResult> GetAdjustmentIndexById(
        [FromQuery] GetAdjustmentIndexByIdRequest request, CT ct)
    {
        var result = await _logic.GetAdjustmentIndexById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetAdjustmentIndexes")]
    [ResponseSchema<GetAdjustmentIndexesResponse>]
    public async Task<IResult> GetAdjustmentIndexes(
        [FromQuery] GetAdjustmentIndexesRequest request, CT ct)
    {
        var result = await _logic.GetAdjustmentIndexes(request, ct);
        return result.GetHttpResponse();
    }
}

