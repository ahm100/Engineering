using Engineering.Application.Services.Transportations;
using Engineering.Application.Services.Transportations.Models.Active;
using Engineering.Application.Services.Transportations.Models.CodeCreator;
using Engineering.Application.Services.Transportations.Models.Create;
using Engineering.Application.Services.Transportations.Models.Disable;
using Engineering.Application.Services.Transportations.Models.GetByCode;
using Engineering.Application.Services.Transportations.Models.GetById;
using Engineering.Application.Services.Transportations.Models.GetByName;
using Engineering.Application.Services.Transportations.Models.GetsActive;
using Engineering.Application.Services.Transportations.Models.GetsFiltered;
using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelEnum;
using Engineering.Application.Services.Transportations.Models.GetsTransportationExcelExporter;
using Engineering.Application.Services.Transportations.Models.GetTransportationTypes;
using Engineering.Application.Services.Transportations.Models.Inactive;
using Engineering.Application.Services.Transportations.Models.StateChangerTransportations;
using Engineering.Application.Services.Transportations.Models.TransportationGroupDelete;
using Engineering.Application.Services.Transportations.Models.Update;

[ApiController]
[Route("api/engineering/v1/Transportation")]
public class TransportationController : ControllerBase
{
    private readonly ITransportationLogic _logic;

    public TransportationController(ITransportationLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddTransportation")]
    [ResponseSchema<CreateTransportationResponse>]
    public async Task<IResult> AddTransportation(
    [FromBody] CreateTransportationRequest request,
    CT ct)
    {
        var result = await _logic.CreateTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TransportationCodeCreator")]
    [ResponseSchema<TransportationCodeCreatorResponse>]
    public async Task<IResult> TransportationCodeCreator(
        [FromBody] TransportationCodeCreatorRequest request,
        CT ct)
    {
        var result = await _logic.TransportationCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TransportationGroupDelete")]
    [ResponseSchema<TransportationGroupDeleteResponse>]
    public async Task<IResult> TransportationGroupDelete(
        [FromBody] TransportationGroupDeleteRequest request,
        CT ct)
    {
        var result = await _logic.TransportationGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateTransportations")]
    [ResponseSchema<StateChangerTransportationsResponse>]
    public async Task<IResult> ActivateTransportations(
        [FromBody] ActivateTransportationsRequest request,
        CT ct)
    {
        var result = await _logic.StateChangerTransportations(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateTransportations")]
    [ResponseSchema<StateChangerTransportationsResponse>]
    public async Task<IResult> InactivateTransportations(
        [FromBody] InactivateTransportationsRequest request,
        CT ct)
    {
        var result = await _logic.StateChangerTransportations(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditTransportation")]
    [ResponseSchema<UpdateTransportationResponse>]
    public async Task<IResult> EditTransportation(
        [FromBody] UpdateTransportationRequest request,
        CT ct)
    {
        var result = await _logic.UpdateTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveTransportation")]
    [ResponseSchema<ActiveTransportationResponse>]
    public async Task<IResult> ActiveTransportation(
        [FromBody] ActiveTransportationRequest request,
        CT ct)
    {
        var result = await _logic.ActiveTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveTransportation")]
    [ResponseSchema<InactiveTransportationResponse>]
    public async Task<IResult> InactiveTransportation(
        [FromBody] InactiveTransportationRequest request,
        CT ct)
    {
        var result = await _logic.InactiveTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationById")]
    [ResponseSchema<GetTransportationByIdResponse>]
    public async Task<IResult> GetTransportationById(
        [FromQuery] GetTransportationByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetTransportationById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationByName")]
    [ResponseSchema<GetTransportationByNameResponse>]
    public async Task<IResult> GetTransportationByName(
        [FromQuery] GetTransportationByNameRequest request,
        CT ct)
    {
        var result = await _logic.GetTransportationByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationByCode")]
    [ResponseSchema<GetTransportationByCodeResponse>]
    public async Task<IResult> GetTransportationByCode(
        [FromQuery] GetTransportationByCodeRequest request,
        CT ct)
    {
        var result = await _logic.GetTransportationByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveTransportationList")]
    [ResponseSchema<GetsActiveTransportationResponse>]
    public async Task<IResult> GetsActiveTransportationList(
        [FromQuery] GetsActiveTransportationRequest request,
        CT ct)
    {
        var result = await _logic.GetsActiveTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsFilteredTransportation")]
    [ResponseSchema<GetsFilteredTransportationResponse>]
    public async Task<IResult> GetsFilteredTransportation(
        [FromQuery] GetsFilteredTransportationRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTransportationExcelExporter")]
    [ResponseSchema<GetsTransportationExcelExporterResponse>]
    public async Task<IResult> GetsTransportationExcelExporter(
    [FromBody] GetsTransportationExcelExporterRequest request,
    CT ct)
    {
        var result = await _logic.GetsTransportationExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTransportationExcelEnum")]
    [ResponseSchema<GetsTransportationExcelEnumResponse>]
    public async Task<IResult> GetsTransportationExcelEnum(
        [FromQuery] GetsTransportationExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableTransportation")]
    [ResponseSchema<DisableTransportationResponse>]
    public async Task<IResult> DisableTransportation(
        [FromQuery] DisableTransportationRequest request,
        CT ct)
    {
        var result = await _logic.DisableTransportation(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationTypes")]
    [ResponseSchema<GetTransportationTypeResponse>]
    public async Task<IResult> GetTransportationTypes(
        [FromQuery] GetTransportationTypeRequest request,
        CT ct)
    {
        var result = await _logic.GetTransportationTypes(request, ct);
        return result.GetHttpResponse();
    }
}