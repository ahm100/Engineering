using Engineering.Application.Services.ShippingCosts;
using Engineering.Application.Services.ShippingCosts.Contracts.ChangeShippingCostState;
using Engineering.Application.Services.ShippingCosts.Contracts.CreateShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.DeleteShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsActiveShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsFilteredShippingCost;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShippingCostById;
using Engineering.Application.Services.ShippingCosts.Contracts.GetShppingCostHistory;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelEnum;
using Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelExporter;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcel;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Application.Services.ShippingCosts.Contracts.UpdateShippingCost;

namespace Engineering.Api.Controllers.ShippingCosts;

[ApiController]
[Route("api/engineering/v1/ShippingCost")]
public class ShippingCostController : ControllerBase
{
    private readonly IShippingCostLogic _logic;

    public ShippingCostController(IShippingCostLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddShippingCost")]
    [ResponseSchema<CreateShippingCostResponse>]
    public async Task<IResult> AddShippingCost(
    [FromBody] CreateShippingCostRequest request,
    CT ct)
    {
        var result = await _logic.CreateShippingCost(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ShippingCostDelete")]
    [ResponseSchema<DeleteShippingCostResponse>]
    public async Task<IResult> ShippingCostDelete(
        [FromBody] DeleteShippingCostRequest request,
        CT ct)
    {
        var result = await _logic.DeleteShippingCost(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditShippingCost")]
    [ResponseSchema<UpdateShippingCostResponse>]
    public async Task<IResult> EditShippingCost(
        [FromBody] UpdateShippingCostRequest request,
        CT ct)
    {
        var result = await _logic.UpdateShippingCost(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeShippingCostState")]
    [ResponseSchema<ChangeShippingCostStateResponse>]
    public async Task<IResult> ChangeShippingCostState(
        [FromBody] ChangeShippingCostStateRequest request,
        CT ct)
    {
        var result = await _logic.ChangeShippingCostState(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveShippingCost")]
    [ResponseSchema<ChangeShippingCostStateResponse>]
    public async Task<IResult> ActiveShippingCost(
        [FromBody] ActiveShippingCostRequest request,
        CT ct)
    {
        var result = await _logic.ChangeShippingCostState(new([request.Id], true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveShippingCost")]
    [ResponseSchema<ChangeShippingCostStateResponse>]
    public async Task<IResult> InactiveShippingCost(
        [FromBody] InActiveShippingCostRequest request,
        CT ct)
    {
        var result = await _logic.ChangeShippingCostState(new([request.Id], false), ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetShippingCostById")]
    [ResponseSchema<GetShippingCostByIdResponse>]
    public async Task<IResult> GetShippingCostById(
        [FromQuery] GetShippingCostByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetShippingCostById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveShippingCostList")]
    [ResponseSchema<GetsActiveShippingCostResponse>]
    public async Task<IResult> GetsActiveShippingCostList(
        [FromBody] GetsActiveShippingCostRequest request,
        CT ct)
    {
        var result = await _logic.GetsActiveShippingCost(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredShippingCost")]
    [ResponseSchema<GetsFilteredShippingCostResponse>]
    public async Task<IResult> GetsFilteredShippingCost(
        [FromBody] GetsFilteredShippingCostRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredShippingCost(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsShippingCostExcelEnum")]
    [ResponseSchema<GetsShippingCostExcelEnumResponse>]
    public async Task<IResult> GetsShippingCostExcelEnum(
        [FromQuery] GetsShippingCostExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsShippingCostExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsShippingCostExcelExporter")]
    [ResponseSchema<GetsShippingCostExcelExporterResponse>]
    public async Task<IResult> GetsShippingCostExcelExporter(
        [FromBody] GetsShippingCostExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsShippingCostExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ShippingCostImportExcel")]
    [ResponseSchema<ShippingCostImportExcelResponse>]
    public async Task<IResult> ShippingCostImportExcel(
        [FromBody] ShippingCostImportExcelRequest request,
        CT ct)
    {
        var result = await _logic.ShippingCostImportExcel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ShippingCostImportExcelHelper")]
    [ResponseSchema<ShippingCostImportExcelHelperResponse>]
    public async Task<IResult> ShippingCostImportExcelHelper(
        [FromBody] ShippingCostImportExcelHelperRequest request,
        CT ct)
    {
        var result = await _logic.ShippingCostImportExcelHelper(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetShippingCostHistory")]
    [ResponseSchema<GetShippingCostHistoryResponse>]
    public async Task<IResult> GetShippingCostHistory(
        [FromQuery] GetShippingCostHistoryRequest request,
        CT ct)
    {
        var result = await _logic.GetShippingCostHistory(request, ct);
        return result.GetHttpResponse();
    }
}