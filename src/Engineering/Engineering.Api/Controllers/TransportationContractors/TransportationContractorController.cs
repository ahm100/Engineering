using Engineering.Application.Services.TransportationContractors;
using Engineering.Application.Services.TransportationContractors.Contracts.ChangeTransportationContractorState;
using Engineering.Application.Services.TransportationContractors.Contracts.CreateTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.DeleteTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsActiveTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsDeliveryMethod;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsDeliveryType;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsFilteredTransportationContractor;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsPriceWeightHistory;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelEnum;
using Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelExporter;
using Engineering.Application.Services.TransportationContractors.Contracts.GetTransportationContractorById;
using Engineering.Application.Services.TransportationContractors.Contracts.PriceWeightImportExcel;
using Engineering.Application.Services.TransportationContractors.Contracts.UpdateTransportationContractor;

namespace Engineering.Api.Controllers.TransportationContractors;

[ApiController]
[Route("api/engineering/v1/TransportationContractor")]
public class TransportationContractorController : ControllerBase
{
    private readonly ITransportationContractorLogic _logic;

    public TransportationContractorController(ITransportationContractorLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddTransportationContractor")]
    [ResponseSchema<CreateTransportationContractorResponse>]
    public async Task<IResult> Create(
    [FromBody] CreateTransportationContractorRequest request,
    CT ct)
    {
        var result = await _logic.CreateTransportationContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TransportationContractorDelete")]
    [ResponseSchema<DeleteTransportationContractorResponse>]
    public async Task<IResult> Delete(
        [FromBody] DeleteTransportationContractorRequest request,
        CT ct)
    {
        var result = await _logic.DeleteTransportationContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditTransportationContractor")]
    [ResponseSchema<UpdateTransportationContractorResponse>]
    public async Task<IResult> Update(
        [FromBody] UpdateTransportationContractorRequest request,
        CT ct)
    {
        var result = await _logic.UpdateTransportationContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeTransportationContractorState")]
    [ResponseSchema<ChangeTransportationContractorStateResponse>]
    public async Task<IResult> ChangeState(
        [FromBody] ChangeTransportationContractorStateRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorState(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveTransportationContractor")]
    [ResponseSchema<ChangeTransportationContractorStateResponse>]
    public async Task<IResult> Active(
        [FromBody] ActiveTransportationContractorRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorState(new([request.Id], true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveTransportationContractor")]
    [ResponseSchema<ChangeTransportationContractorStateResponse>]
    public async Task<IResult> Inactive(
        [FromBody] InActiveTransportationContractorRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorState(new([request.Id], false), ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationContractorById")]
    [ResponseSchema<GetTransportationContractorByIdResponse>]
    public async Task<IResult> GetById(
        [FromQuery] GetTransportationContractorByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetTransportationContractorById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveTransportationContractorList")]
    [ResponseSchema<GetsActiveTransportationContractorResponse>]
    public async Task<IResult> GetActiveList(
        [FromBody] GetsActiveTransportationContractorRequest request,
        CT ct)
    {
        var result = await _logic.GetsActiveTransportationContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredTransportationContractor")]
    [ResponseSchema<GetsFilteredTransportationContractorResponse>]
    public async Task<IResult> GetFiltered(
        [FromBody] GetsFilteredTransportationContractorRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredTransportationContractor(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTransportationContractorExcelEnum")]
    [ResponseSchema<GetsTransportationContractorExcelEnumResponse>]
    public async Task<IResult> GetExcelEnum(
        [FromQuery] GetsTransportationContractorExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationContractorExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTransportationContractorExcelExporter")]
    [ResponseSchema<GetsTransportationContractorExcelExporterResponse>]
    public async Task<IResult> ExportExcel(
        [FromBody] GetsTransportationContractorExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationContractorExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("PriceWeightImportExcel")]
    [ResponseSchema<PriceWeightImportExcelResponse>]
    public async Task<IResult> ImportPriceWeightExcel(
        [FromBody] PriceWeightImportExcelRequest request,
        CT ct)
    {
        var result = await _logic.PriceWeightImportExcel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsDeliveryMethod")]
    [ResponseSchema<GetsDeliveryMethodResponse>]
    public async Task<IResult> GetDeliveryMethod(
        [FromQuery] GetsDeliveryMethodRequest request,
        CT ct)
    {
        var result = await _logic.GetsDeliveryMethod(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsDeliveryType")]
    [ResponseSchema<GetsDeliveryTypeResponse>]
    public async Task<IResult> GetDeliveryType(
        [FromQuery] GetsDeliveryTypeRequest request,
        CT ct)
    {
        var result = await _logic.GetsDeliveryType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsPriceWeightHistory")]
    [ResponseSchema<GetsPriceWeightHistoryResponse>]
    public async Task<IResult> GetPriceWeightHistory(
        [FromQuery] GetsPriceWeightHistoryRequest request,
        CT ct)
    {
        var result = await _logic.GetsPriceWeightHistory(request, ct);
        return result.GetHttpResponse();
    }
}