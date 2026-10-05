using Engineering.Application.Services.ContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.ActiveContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.ContractorMachineryGroupDelete;
using Engineering.Application.Services.ContractorMachineries.Models.CreateContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.DisableContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.GetActiveContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryById;
using Engineering.Application.Services.ContractorMachineries.Models.GetContractorMachineryUnit;
using Engineering.Application.Services.ContractorMachineries.Models.GetsByContractorId;
using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelEnum;
using Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelExporter;
using Engineering.Application.Services.ContractorMachineries.Models.InactiveContractorMachinery;
using Engineering.Application.Services.ContractorMachineries.Models.StateChangerContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.UpdateContractorMachinery;

[Authorize]
[Route("api/engineering/v1/ContractorMachinery")]
public class ContractorMachineryController : ControllerBase
{
    private readonly ILogger<ContractorMachineryController> _logger;
    private readonly IContractorMachineryLogic _logic;

    public ContractorMachineryController(
        ILogger<ContractorMachineryController> logger,
        IContractorMachineryLogic logic)
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("CreateContractorMachinery")]
    [ResponseSchema<CreateContractorMachineryResponse>]
    public async Task<IResult> CreateContractorMachinery(
        [FromBody] CreateContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("CreateContractorMachinery");
        var result = await _logic.CreateContractorMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("ContractorMachineryGroupDelete")]
    [ResponseSchema<ContractorMachineryGroupDeleteResponse>]
    public async Task<IResult> ContractorMachineryGroupDelete(
        [FromBody] ContractorMachineryGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("ContractorMachineryGroupDelete");
        var result = await _logic.ContractorMachineryGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateContractorMachineries")]
    [ResponseSchema<StateChangerContractorMachineriesResponse>]
    public async Task<IResult> ActivateContractorMachineries(
        [FromBody] ActivateContractorMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("ActivateContractorMachineries");
        var result = await _logic.StateChangerContractorMachineries(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateContractorMachineries")]
    [ResponseSchema<StateChangerContractorMachineriesResponse>]
    public async Task<IResult> InactivateContractorMachineries(
        [FromBody] InactivateContractorMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("InactivateContractorMachineries");
        var result = await _logic.StateChangerContractorMachineries(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateContractorMachinery")]
    [ResponseSchema<UpdateContractorMachineryResponse>]
    public async Task<IResult> UpdateContractorMachinery(
        [FromBody] UpdateContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("UpdateContractorMachinery");
        var result = await _logic.UpdateContractorMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveContractorMachinery")]
    [ResponseSchema<ActiveContractorMachineryResponse>]
    public async Task<IResult> ActiveContractorMachinery(
        [FromBody] ActiveContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("ActiveContractorMachinery");
        var result = await _logic.ActiveContractorMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveContractorMachinery")]
    [ResponseSchema<InactiveContractorMachineryResponse>]
    public async Task<IResult> InactiveContractorMachinery(
        [FromBody] InactiveContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("InactiveContractorMachinery");
        var result = await _logic.InactiveContractorMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorMachineryById")]
    [ResponseSchema<GetContractorMachineryByIdResponse>]
    public async Task<IResult> GetContractorMachineryById(
        [FromQuery] GetContractorMachineryByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetContractorMachineryById");
        var result = await _logic.GetContractorMachineryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveContractorMachinery")]
    [ResponseSchema<GetActiveContractorMachineriesResponse>]
    public async Task<IResult> GetsActiveContractorMachinery(
        [FromBody] GetActiveContractorMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("GetsActiveContractorMachinery");
        var result = await _logic.GetsActiveContractorMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorMachinery")]
    [ResponseSchema<GetContractorMachineriesResponse>]
    public async Task<IResult> GetsContractorMachinery(
        [FromBody] GetContractorMachineriesRequest request, CT ct)
    {
        _logger.LogInformation("GetsContractorMachinery");
        var result = await _logic.GetsContractorMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsByContractorId")]
    [ResponseSchema<GetsByContractorIdResponse>]
    public async Task<IResult> GetsByContractorId(
        [FromBody] GetsByContractorIdRequest request, CT ct)
    {
        _logger.LogInformation("GetsByContractorId");
        var result = await _logic.GetsByContractorId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsContractorMachineryExcelExporter")]
    [ResponseSchema<GetsContractorMachineryExcelExporterResponse>]
    public async Task<IResult> GetsContractorMachineryExcelExporter(
        [FromBody] GetsContractorMachineryExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetsContractorMachineryExcelExporter");
        var result = await _logic.GetsContractorMachineryExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsContractorMachineryExcelEnum")]
    [ResponseSchema<GetsContractorMachineryExcelEnumResponse>]
    public async Task<IResult> GetsContractorMachineryExcelEnum(
        [FromQuery] GetsContractorMachineryExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetsContractorMachineryExcelEnum");
        var result = await _logic.GetsContractorMachineryExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetContractorMachineryUnit")]
    [ResponseSchema<GetContractorMachineryUnitResponse>]
    public async Task<IResult> GetContractorMachineryUnit(
        [FromQuery] GetContractorMachineryUnitRequest request, CT ct)
    {
        _logger.LogInformation("GetContractorMachineryUnit");
        var result = await _logic.GetContractorMachineryUnit(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableContractorMachinery")]
    [ResponseSchema<DisableContractorMachineryResponse>]
    public async Task<IResult> DisableContractorMachinery(
        [FromQuery] DisableContractorMachineryRequest request, CT ct)
    {
        _logger.LogInformation("DisableContractorMachinery");
        var result = await _logic.DisableContractorMachinery(request, ct);
        return result.GetHttpResponse();
    }
}