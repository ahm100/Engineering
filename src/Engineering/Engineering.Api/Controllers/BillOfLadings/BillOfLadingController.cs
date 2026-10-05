using Engineering.Application.Services.BillOfLadings;
using Engineering.Application.Services.BillOfLadings.Contracts.BillOfLadingCodeCreator;
using Engineering.Application.Services.BillOfLadings.Contracts.ChangeBillOfLadingState;
using Engineering.Application.Services.BillOfLadings.Contracts.CreateBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.DeleteBillOfLadings;
using Engineering.Application.Services.BillOfLadings.Contracts.GetBillOfLadingById;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsActiveBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelEnum;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsBillOfLadingExcelExporter;
using Engineering.Application.Services.BillOfLadings.Contracts.GetsFilteredBillOfLading;
using Engineering.Application.Services.BillOfLadings.Contracts.UpdateBillOfLading;

[Authorize]
[Route("api/engineering/v1/BillOfLading")]
public class BillOfLadingController : ControllerBase
{
    private readonly ILogger<BillOfLadingController> _logger;
    private readonly IBillOfLadingLogic _logic;

    public BillOfLadingController(
        ILogger<BillOfLadingController> logger,
        IBillOfLadingLogic logic) : base()
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("AddBillOfLading")]
    [ResponseSchema<CreateBillOfLadingResponse>]
    public async Task<IResult> AddBillOfLading(
        [FromBody] CreateBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("AddBillOfLading");
        var result = await _logic.CreateBillOfLading(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("BillOfLadingCodeCreator")]
    [ResponseSchema<BillOfLadingCodeCreatorResponse>]
    public async Task<IResult> BillOfLadingCodeCreator(
        [FromBody] BillOfLadingCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("BillOfLadingCodeCreator");
        var result = await _logic.BillOfLadingCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("BillOfLadingGroupDelete")]
    [ResponseSchema<DeleteBillOfLadingsResponse>]
    public async Task<IResult> BillOfLadingGroupDelete(
        [FromBody] DeleteBillOfLadingsRequest request, CT ct)
    {
        _logger.LogInformation("BillOfLadingGroupDelete");
        var result = await _logic.DeleteBillOfLadings(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditBillOfLading")]
    [ResponseSchema<UpdateBillOfLadingResponse>]
    public async Task<IResult> EditBillOfLading(
        [FromBody] UpdateBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("EditBillOfLading");
        var result = await _logic.UpdateBillOfLading(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeBillOfLadingState")]
    [ResponseSchema<ChangeBillOfLadingStateResponse>]
    public async Task<IResult> ChangeBillOfLadingState(
        [FromBody] ChangeBillOfLadingStateRequest request, CT ct)
    {
        _logger.LogInformation("ChangeBillOfLadingState");
        var result = await _logic.ChangeBillOfLadingState(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveBillOfLading")]
    [ResponseSchema<ChangeBillOfLadingStateResponse>]
    public async Task<IResult> ActiveBillOfLading(
        [FromBody] ActiveBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("ActiveBillOfLading");
        var result = await _logic.ChangeBillOfLadingState(new([request.Id], true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveBillOfLading")]
    [ResponseSchema<ChangeBillOfLadingStateResponse>]
    public async Task<IResult> InactiveBillOfLading(
        [FromBody] InActiveBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("InactiveBillOfLading");
        var result = await _logic.ChangeBillOfLadingState(new([request.Id], false), ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetBillOfLadingById")]
    [ResponseSchema<GetBillOfLadingByIdResponse>]
    public async Task<IResult> GetBillOfLadingById(
        [FromQuery] GetBillOfLadingByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetBillOfLadingById");
        var result = await _logic.GetBillOfLadingById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveBillOfLadingList")]
    [ResponseSchema<GetsActiveBillOfLadingResponse>]
    public async Task<IResult> GetsActiveBillOfLadingList(
        [FromQuery] GetsActiveBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("GetsActiveBillOfLadingList");
        var result = await _logic.GetsActiveBillOfLading(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsFilteredBillOfLading")]
    [ResponseSchema<GetsFilteredBillOfLadingResponse>]
    public async Task<IResult> GetsFilteredBillOfLading(
        [FromQuery] GetsFilteredBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("GetsFilteredBillOfLading");
        var result = await _logic.GetsFilteredBillOfLading(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsBillOfLadingExcelEnum")]
    [ResponseSchema<GetsBillOfLadingExcelEnumResponse>]
    public async Task<IResult> GetsBillOfLadingExcelEnum(
        [FromQuery] GetsBillOfLadingExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetsBillOfLadingExcelEnum");
        var result = await _logic.GetsBillOfLadingExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsBillOfLadingExcelExporter")]
    [ResponseSchema<GetsBillOfLadingExcelExporterResponse>]
    public async Task<IResult> GetsBillOfLadingExcelExporter(
        [FromBody] GetsBillOfLadingExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetsBillOfLadingExcelExporter");
        var result = await _logic.GetsBillOfLadingExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableBillOfLading")]
    [ResponseSchema<DeleteBillOfLadingsResponse>]
    public async Task<IResult> DisableBillOfLading(
        [FromBody] DeleteBillOfLadingRequest request, CT ct)
    {
        _logger.LogInformation("DisableBillOfLading");
        var newRequest = new DeleteBillOfLadingsRequest([request.Id]);
        var result = await _logic.DeleteBillOfLadings(newRequest, ct);
        return result.GetHttpResponse();
    }
}