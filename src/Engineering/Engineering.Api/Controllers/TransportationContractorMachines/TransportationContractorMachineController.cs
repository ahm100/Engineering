using Engineering.Application.Services.TransportationContractorMachines;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.ChangeTransportationContractorMachineState;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.CreateTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.DeleteTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsActiveTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsFilteredTransportationContractorMachine;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelEnum;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelExporter;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.GetTransportationContractorMachineById;
using Engineering.Application.Services.TransportationContractorMachines.Contracts.UpdateTransportationContractorMachine;

[ApiController]
[Route("api/engineering/v1/TransportationContractorMachine")]
public class TransportationContractorMachineController : ControllerBase
{
    private readonly ITransportationContractorMachineLogic _logic;

    public TransportationContractorMachineController(ITransportationContractorMachineLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("AddTransportationContractorMachine")]
    [ResponseSchema<CreateTransportationContractorMachineResponse>]
    public async Task<IResult> Create(
    [FromBody] CreateTransportationContractorMachineRequest request,
    CT ct)
    {
        var result = await _logic.CreateTransportationContractorMachine(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("TransportationContractorMachineDelete")]
    [ResponseSchema<DeleteTransportationContractorMachineResponse>]
    public async Task<IResult> Delete(
        [FromBody] DeleteTransportationContractorMachineRequest request,
        CT ct)
    {
        var result = await _logic.DeleteTransportationContractorMachine(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditTransportationContractorMachine")]
    [ResponseSchema<UpdateTransportationContractorMachineResponse>]
    public async Task<IResult> Update(
        [FromBody] UpdateTransportationContractorMachineRequest request,
        CT ct)
    {
        var result = await _logic.UpdateTransportationContractorMachine(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ChangeTransportationContractorMachineState")]
    [ResponseSchema<ChangeTransportationContractorMachineStateResponse>]
    public async Task<IResult> ChangeState(
        [FromBody] ChangeTransportationContractorMachineStateRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorMachineState(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveTransportationContractorMachine")]
    [ResponseSchema<ChangeTransportationContractorMachineStateResponse>]
    public async Task<IResult> Active(
        [FromBody] ActiveTransportationContractorMachineRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorMachineState(new([request.Id], true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveTransportationContractorMachine")]
    [ResponseSchema<ChangeTransportationContractorMachineStateResponse>]
    public async Task<IResult> Inactive(
        [FromBody] InActiveTransportationContractorMachineRequest request,
        CT ct)
    {
        var result = await _logic.ChangeTransportationContractorMachineState(new([request.Id], false), ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetTransportationContractorMachineById")]
    [ResponseSchema<GetTransportationContractorMachineByIdResponse>]
    public async Task<IResult> GetById(
        [FromQuery] GetTransportationContractorMachineByIdRequest request,
        CT ct)
    {
        var result = await _logic.GetTransportationContractorMachineById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveTransportationContractorMachineList")]
    [ResponseSchema<GetsActiveTransportationContractorMachineResponse>]
    public async Task<IResult> GetActiveList(
        [FromBody] GetsActiveTransportationContractorMachineRequest request,
        CT ct)
    {
        var result = await _logic.GetsActiveTransportationContractorMachine(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFilteredTransportationContractorMachine")]
    [ResponseSchema<GetsFilteredTransportationContractorMachineResponse>]
    public async Task<IResult> GetFiltered(
        [FromBody] GetsFilteredTransportationContractorMachineRequest request,
        CT ct)
    {
        var result = await _logic.GetsFilteredTransportationContractorMachine(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsTransportationContractorMachineExcelEnum")]
    [ResponseSchema<GetsTransportationContractorMachineExcelEnumResponse>]
    public async Task<IResult> GetExcelEnum(
        [FromQuery] GetsTransportationContractorMachineExcelEnumRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationContractorMachineExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsTransportationContractorMachineExcelExporter")]
    [ResponseSchema<GetsTransportationContractorMachineExcelExporterResponse>]
    public async Task<IResult> ExportExcel(
        [FromBody] GetsTransportationContractorMachineExcelExporterRequest request,
        CT ct)
    {
        var result = await _logic.GetsTransportationContractorMachineExcelExporter(request, ct);
        return result.GetHttpResponse();
    }
}