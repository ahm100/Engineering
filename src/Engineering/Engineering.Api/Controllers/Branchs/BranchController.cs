using Engineering.Application.Services.Branchs;
using Engineering.Application.Services.Branchs.Models.ActiveBranch;
using Engineering.Application.Services.Branchs.Models.BranchGroupDelete;
using Engineering.Application.Services.Branchs.Models.CodeCreator;
using Engineering.Application.Services.Branchs.Models.CreateBranch;
using Engineering.Application.Services.Branchs.Models.DisableBranch;
using Engineering.Application.Services.Branchs.Models.GetBranchByCode;
using Engineering.Application.Services.Branchs.Models.GetBranchById;
using Engineering.Application.Services.Branchs.Models.GetBranchByName;
using Engineering.Application.Services.Branchs.Models.GetsActiveBranchs;
using Engineering.Application.Services.Branchs.Models.GetsBranchByCategoryIds;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelEnum;
using Engineering.Application.Services.Branchs.Models.GetsBranchExcelExporter;
using Engineering.Application.Services.Branchs.Models.GetsBranchs;
using Engineering.Application.Services.Branchs.Models.GetsByCategoryId;
using Engineering.Application.Services.Branchs.Models.InactiveBranch;
using Engineering.Application.Services.Branchs.Models.StateChangerBranchs;
using Engineering.Application.Services.Branchs.Models.UpdateBranch;

[Authorize]
[Route("api/engineering/v1/Branch")]
public class BranchController : ControllerBase
{
    private readonly ILogger<BranchController> _logger;
    private readonly IBranchLogic _logic;

    public BranchController(
        ILogger<BranchController> logger,
        IBranchLogic logic) : base()
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("AddBranch")]
    [ResponseSchema<CreateBranchResponse>]
    public async Task<IResult> AddBranch(
        [FromBody] CreateBranchRequest request, CT ct)
    {
        _logger.LogInformation("AddBranch");
        var result = await _logic.CreateBranch(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("BranchCodeCreator")]
    [ResponseSchema<BranchCodeCreatorResponse>]
    public async Task<IResult> BranchCodeCreator(
        [FromBody] BranchCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("BranchCodeCreator");
        var result = await _logic.BranchCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("BranchGroupDelete")]
    [ResponseSchema<BranchGroupDeleteResponse>]
    public async Task<IResult> BranchGroupDelete(
        [FromBody] BranchGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("BranchGroupDelete");
        var result = await _logic.BranchGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateBranchs")]
    [ResponseSchema<StateChangerBranchsResponse>]
    public async Task<IResult> ActivateBranchs(
        [FromBody] ActivateBranchsRequest request, CT ct)
    {
        _logger.LogInformation("ActivateBranchs");
        var result = await _logic.StateChangerBranchs(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateBranchs")]
    [ResponseSchema<StateChangerBranchsResponse>]
    public async Task<IResult> InactivateBranchs(
        [FromBody] InactivateBranchsRequest request, CT ct)
    {
        _logger.LogInformation("InactivateBranchs");
        var result = await _logic.StateChangerBranchs(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditBranch")]
    [ResponseSchema<UpdateBranchResponse>]
    public async Task<IResult> EditBranch(
        [FromBody] UpdateBranchRequest request, CT ct)
    {
        _logger.LogInformation("EditBranch");
        var result = await _logic.UpdateBranch(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveBranch")]
    [ResponseSchema<ActiveBranchResponse>]
    public async Task<IResult> ActiveBranch(
        [FromBody] ActiveBranchRequest request, CT ct)
    {
        _logger.LogInformation("ActiveBranch");
        var result = await _logic.ActiveBranch(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveBranch")]
    [ResponseSchema<InactiveBranchResponse>]
    public async Task<IResult> InactiveBranch(
        [FromBody] InactiveBranchRequest request, CT ct)
    {
        _logger.LogInformation("InactiveBranch");
        var result = await _logic.InactiveBranch(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetBranchById")]
    [ResponseSchema<GetBranchByIdResponse>]
    public async Task<IResult> GetBranchById(
        [FromQuery] GetBranchByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetBranchById");
        var result = await _logic.GetBranchById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetBranchByName")]
    [ResponseSchema<GetBranchByNameResponse>]
    public async Task<IResult> GetBranchByName(
        [FromQuery] GetBranchByNameRequest request, CT ct)
    {
        _logger.LogInformation("GetBranchByName");
        var result = await _logic.GetBranchByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetBranchByCode")]
    [ResponseSchema<GetBranchByCodeResponse>]
    public async Task<IResult> GetBranchByCode(
        [FromQuery] GetBranchByCodeRequest request, CT ct)
    {
        _logger.LogInformation("GetBranchByCode");
        var result = await _logic.GetBranchByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveBranchList")]
    [ResponseSchema<GetsActiveBranchsResponse>]
    public async Task<IResult> GetsActiveBranchList(
        [FromQuery] GetsActiveBranchsRequest request, CT ct)
    {
        _logger.LogInformation("GetsActiveBranchList");
        var result = await _logic.GetsActiveBranch(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsBranch")]
    [ResponseSchema<GetsBranchsResponse>]
    public async Task<IResult> GetsBranch(
        [FromQuery] GetsBranchsRequest request, CT ct)
    {
        _logger.LogInformation("GetsBranch");
        var result = await _logic.GetsBranch(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsByCategoryId")]
    [ResponseSchema<GetsByCategoryIdResponse>]
    public async Task<IResult> GetsByCategoryId(
        [FromQuery] GetsByCategoryIdRequest request, CT ct)
    {
        _logger.LogInformation("GetsByCategoryId");
        var result = await _logic.GetsByCategoryId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsBranchByCategoryIds")]
    [ResponseSchema<GetsBranchByCategoryIdsResponse>]
    public async Task<IResult> GetsBranchByCategoryIds(
        [FromBody] GetsBranchByCategoryIdsRequest request, CT ct)
    {
        _logger.LogInformation("GetsBranchByCategoryIds");
        var result = await _logic.GetsBranchByCategoryIds(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsBranchExcelEnum")]
    [ResponseSchema<GetsBranchExcelEnumResponse>]
    public async Task<IResult> GetsBranchExcelEnum(
        [FromQuery] GetsBranchExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetsBranchExcelEnum");
        var result = await _logic.GetsBranchExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsBranchExcelExporter")]
    [ResponseSchema<GetsBranchExcelExporterResponse>]
    public async Task<IResult> GetsBranchExcelExporter(
        [FromBody] GetsBranchExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetsBranchExcelExporter");
        var result = await _logic.GetsBranchExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableBranch")]
    [ResponseSchema<DisableBranchResponse>]
    public async Task<IResult> DisableBranch(
        [FromQuery] DisableBranchRequest request, CT ct)
    {
        _logger.LogInformation("DisableBranch");
        var result = await _logic.DisableBranch(request, ct);
        return result.GetHttpResponse();
    }
}