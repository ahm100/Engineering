using Engineering.Application.Services.TransportationContractorPersonnels;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.ChangeTransportationContractorPersonnelState;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.CreateTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.DeleteTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsActiveTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsFilteredTransportationContractorPersonnel;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelEnum;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelExporter;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetTransportationContractorPersonnelById;
using Engineering.Application.Services.TransportationContractorPersonnels.Contracts.UpdateTransportationContractorPersonnel;

namespace Engineering.Api.Controllers.TransportationContractorPersonnels;

[ApiController]
[Route("api/engineering/v1/TransportationContractorPersonnel")]
public class TransportationContractorPersonnelController : ControllerBase
{
    private readonly ITransportationContractorPersonnelLogic _logic;

    public TransportationContractorPersonnelController(ITransportationContractorPersonnelLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddTransportationContractorPersonnel")]
    [ResponseSchema<CreateTransportationContractorPersonnelResponse>]
    public async Task<IResult> Create(
    [FromBody] CreateTransportationContractorPersonnelRequest request,
    CT ct)
    {
        var result = await _logic.CreateTransportationContractorPersonnel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TransportationContractorPersonnelDelete")]
    [ResponseSchema<DeleteTransportationContractorPersonnelResponse>]
    public async Task<IResult> Delete(
        [FromBody] DeleteTransportationContractorPersonnelRequest request,
        CT ct)
    {
        var result = await _logic.DeleteTransportationContractorPersonnel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditTransportationContractorPersonnel")]
    [ResponseSchema<UpdateTransportationContractorPersonnelResponse>]
    public async Task<IResult> Update(
        [FromBody] UpdateTransportationContractorPersonnelRequest request,
        CT ct)
    {
        var result = await _logic.UpdateTransportationContractorPersonnel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeTransportationContractorPersonnelState")]
    [ResponseSchema<ChangeTransportationContractorPersonnelStateResponse>]
    public async Task<IResult> ChangeState(
        [FromBody] ChangeTransportationContractorPersonnelStateRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorPersonnelState(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveTransportationContractorPersonnel")]
    [ResponseSchema<ChangeTransportationContractorPersonnelStateResponse>]
    public async Task<IResult> Active(
        [FromBody] ActiveTransportationContractorPersonnelRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorPersonnelState(new([request.Id], true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveTransportationContractorPersonnel")]
    [ResponseSchema<ChangeTransportationContractorPersonnelStateResponse>]
    public async Task<IResult> Inactive(
        [FromBody] InActiveTransportationContractorPersonnelRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorPersonnelState(new([request.Id], false), ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationContractorPersonnelById")]
    [ResponseSchema<GetTransportationContractorPersonnelByIdResponse>]
    public async Task<IResult> GetById(
        [FromQuery] GetTransportationContractorPersonnelByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetTransportationContractorPersonnelById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveTransportationContractorPersonnelList")]
    [ResponseSchema<GetsActiveTransportationContractorPersonnelResponse>]
    public async Task<IResult> GetActiveList(
        [FromBody] GetsActiveTransportationContractorPersonnelRequest request,
        CT ct)
    {
        var result = await _logic.GetsActiveTransportationContractorPersonnel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredTransportationContractorPersonnel")]
    [ResponseSchema<GetsFilteredTransportationContractorPersonnelResponse>]
    public async Task<IResult> GetFiltered(
        [FromBody] GetsFilteredTransportationContractorPersonnelRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredTransportationContractorPersonnel(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTransportationContractorPersonnelExcelEnum")]
    [ResponseSchema<GetsTransportationContractorPersonnelExcelEnumResponse>]
    public async Task<IResult> GetExcelEnum(
        [FromQuery] GetsTransportationContractorPersonnelExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationContractorPersonnelExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTransportationContractorPersonnelExcelExporter")]
    [ResponseSchema<GetsTransportationContractorPersonnelExcelExporterResponse>]
    public async Task<IResult> ExportExcel(
        [FromBody] GetsTransportationContractorPersonnelExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationContractorPersonnelExcelExporter(request, ct);
        return result.GetHttpResponse();
    }
}