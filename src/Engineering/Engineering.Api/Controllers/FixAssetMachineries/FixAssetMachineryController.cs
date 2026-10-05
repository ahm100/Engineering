using Engineering.Application.Services.FixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.ActiveFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.CreateFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.DisableFixAssetMachineryRate;
using Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryGroupDelete;
using Engineering.Application.Services.FixAssetMachineries.Models.FixAssetMachineryNotWorkGroupDelete;
using Engineering.Application.Services.FixAssetMachineries.Models.GetActiveFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorkById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryNotWorks;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryRateById;
using Engineering.Application.Services.FixAssetMachineries.Models.GetFixAssetMachineryType;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelEnum;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelExporter;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelEnum;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelExporter;
using Engineering.Application.Services.FixAssetMachineries.Models.GetsRateByFixAssetMachineryId;
using Engineering.Application.Services.FixAssetMachineries.Models.InactiveFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.StateChangerFixAssetMachineries;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachinery;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachineryNotWork;
using Engineering.Application.Services.FixAssetMachineries.Models.UpdateFixAssetMachineryRate;

namespace Engineering.Api.Controllers;

[ApiController]
[Route("api/engineering/v1/FixAssetMachinery")]
public class FixAssetMachineryController : ControllerBase
{
    private readonly IFixAssetMachineryLogic _logic;

    public FixAssetMachineryController(IFixAssetMachineryLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateFixAssetMachinery")]
    [ResponseSchema<CreateFixAssetMachineryResponse>]
    public async Task<IResult> CreateFixAssetMachinery([FromBody] CreateFixAssetMachineryRequest request, CT ct)
    {
        var result = await _logic.CreateFixAssetMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateFixAssetMachineryNotWork")]
    [ResponseSchema<CreateFixAssetMachineryNotWorkResponse>]
    public async Task<IResult> CreateFixAssetMachineryNotWork([FromBody] CreateFixAssetMachineryNotWorkRequest request, CT ct)
    {
        var result = await _logic.CreateFixAssetMachineryNotWork(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("FixAssetMachineryGroupDelete")]
    [ResponseSchema<FixAssetMachineryGroupDeleteResponse>]
    public async Task<IResult> FixAssetMachineryGroupDelete([FromBody] FixAssetMachineryGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.FixAssetMachineryGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("FixAssetMachineryNotworkGroupDelete")]
    [ResponseSchema<FixAssetMachineryNotWorkGroupDeleteResponse>]
    public async Task<IResult> FixAssetMachineryNotworkGroupDelete([FromBody] FixAssetMachineryNotWorkGroupDeleteRequest request, CT ct)
    {
        var result = await _logic.FixAssetMachineryNotWorkGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateFixAssetMachineries")]
    [ResponseSchema<StateChangerFixAssetMachineriesResponse>]
    public async Task<IResult> ActivateFixAssetMachineries([FromBody] ActivateFixAssetMachineriesRequest request, CT ct)
    {
        var result = await _logic.StateChangerFixAssetMachineries(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateFixAssetMachineries")]
    [ResponseSchema<StateChangerFixAssetMachineriesResponse>]
    public async Task<IResult> InactivateFixAssetMachineries([FromBody] InactivateFixAssetMachineriesRequest request, CT ct)
    {
        var result = await _logic.StateChangerFixAssetMachineries(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateFixAssetMachinery")]
    [ResponseSchema<UpdateFixAssetMachineryResponse>]
    public async Task<IResult> UpdateFixAssetMachinery([FromBody] UpdateFixAssetMachineryRequest request, CT ct)
    {
        var result = await _logic.UpdateFixAssetMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateFixAssetMachineryNotWork")]
    [ResponseSchema<UpdateFixAssetMachineryNotWorkResponse>]
    public async Task<IResult> UpdateFixAssetMachineryNotWork([FromBody] UpdateFixAssetMachineryNotWorkRequest request, CT ct)
    {
        var result = await _logic.UpdateFixAssetMachineryNotWork(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveFixAssetMachinery")]
    [ResponseSchema<ActiveFixAssetMachineryResponse>]
    public async Task<IResult> ActiveFixAssetMachinery([FromBody] ActiveFixAssetMachineryRequest request, CT ct)
    {
        var result = await _logic.ActiveFixAssetMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveFixAssetMachinery")]
    [ResponseSchema<InactiveFixAssetMachineryResponse>]
    public async Task<IResult> InactiveFixAssetMachinery([FromBody] InactiveFixAssetMachineryRequest request, CT ct)
    {
        var result = await _logic.InactiveFixAssetMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFixAssetMachineryById")]
    [ResponseSchema<GetFixAssetMachineryByIdResponse>]
    public async Task<IResult> GetFixAssetMachineryById([FromQuery] GetFixAssetMachineryByIdRequest request, CT ct)
    {
        var result = await _logic.GetFixAssetMachineryById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFixAssetMachineryNotWorkById")]
    [ResponseSchema<GetFixAssetMachineryNotWorkByIdResponse>]
    public async Task<IResult> GetFixAssetMachineryNotWorkById([FromQuery] GetFixAssetMachineryNotWorkByIdRequest request, CT ct)
    {
        var result = await _logic.GetFixAssetMachineryNotWorkById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsActiveFixAssetMachinery")]
    [ResponseSchema<GetActiveFixAssetMachineriesResponse>]
    public async Task<IResult> GetsActiveFixAssetMachinery([FromBody] GetActiveFixAssetMachineriesRequest request, CT ct)
    {
        var result = await _logic.GetsActiveFixAssetMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFixAssetMachinery")]
    [ResponseSchema<GetFixAssetMachineriesResponse>]
    public async Task<IResult> GetsFixAssetMachinery([FromBody] GetFixAssetMachineriesRequest request, CT ct)
    {
        var result = await _logic.GetsFixAssetMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFixAssetMachineryNotWork")]
    [ResponseSchema<GetFixAssetMachineryNotWorksResponse>]
    public async Task<IResult> GetsFixAssetMachineryNotWork([FromBody] GetFixAssetMachineryNotWorksRequest request, CT ct)
    {
        var result = await _logic.GetsFixAssetMachineryNotWork(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFixAssetMachineryNotWorkExcelExporter")]
    [ResponseSchema<GetsFixAssetMachineryNotWorkExcelExporterResponse>]
    public async Task<IResult> GetsFixAssetMachineryNotWorkExcelExporter([FromBody] GetsFixAssetMachineryNotWorkExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsFixAssetMachineryNotWorkExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsFixAssetMachineryNotworkExcelEnum")]
    [ResponseSchema<GetsFixAssetMachineryNotWorkExcelEnumResponse>]
    public async Task<IResult> GetsFixAssetMachineryNotworkExcelEnum([FromQuery] GetsFixAssetMachineryNotWorkExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsFixAssetMachineryNotworkExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsFixAssetMachineryExcelExporter")]
    [ResponseSchema<GetsFixAssetMachineryExcelExporterResponse>]
    public async Task<IResult> GetsFixAssetMachineryExcelExporter([FromBody] GetsFixAssetMachineryExcelExporterRequest request, CT ct)
    {
        var result = await _logic.GetsFixAssetMachineryExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsFixAssetMachineryExcelEnum")]
    [ResponseSchema<GetsFixAssetMachineryExcelEnumResponse>]
    public async Task<IResult> GetsFixAssetMachineryExcelEnum([FromQuery] GetsFixAssetMachineryExcelEnumRequest request, CT ct)
    {
        var result = await _logic.GetsFixAssetMachineryExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFixAssetMachineryType")]
    [ResponseSchema<GetFixAssetMachineryTypeResponse>]
    public async Task<IResult> GetFixAssetMachineryType([FromQuery] GetFixAssetMachineryTypeRequest request, CT ct)
    {
        var result = await _logic.GetFixAssetMachineryType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableFixAssetMachinery")]
    [ResponseSchema<DisableFixAssetMachineryResponse>]
    public async Task<IResult> DisableFixAssetMachinery([FromBody] DisableFixAssetMachineryRequest request, CT ct)
    {
        var result = await _logic.DisableFixAssetMachinery(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DisableFixAssetMachineryNotWork")]
    [ResponseSchema<DisableFixAssetMachineryNotWorkResponse>]
    public async Task<IResult> DisableFixAssetMachineryNotWork([FromBody] DisableFixAssetMachineryNotWorkRequest request, CT ct)
    {
        var result = await _logic.DisableFixAssetMachineryNotWork(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateFixAssetMachineryRate")]
    [ResponseSchema<CreateFixAssetMachineryRateResponse>]
    public async Task<IResult> CreateFixAssetMachineryRate([FromBody] CreateFixAssetMachineryRateRequest request, CT ct)
    {
        var result = await _logic.CreateFixAssetMachineryRate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DisableFixAssetMachineryRate")]
    [ResponseSchema<DisableFixAssetMachineryRateResponse>]
    public async Task<IResult> DisableFixAssetMachineryRate([FromBody] DisableFixAssetMachineryRateRequest request, CT ct)
    {
        var result = await _logic.DisableFixAssetMachineryRate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("UpdateFixAssetMachineryRate")]
    [ResponseSchema<UpdateFixAssetMachineryRateResponse>]
    public async Task<IResult> UpdateFixAssetMachineryRate([FromBody] UpdateFixAssetMachineryRateRequest request, CT ct)
    {
        var result = await _logic.UpdateFixAssetMachineryRate(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetFixAssetMachineryRateById")]
    [ResponseSchema<GetFixAssetMachineryRateByIdResponse>]
    public async Task<IResult> GetFixAssetMachineryRateById([FromQuery] GetFixAssetMachineryRateByIdRequest request, CT ct)
    {
        var result = await _logic.GetFixAssetMachineryRateById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsRateByFixAssetMachineryId")]
    [ResponseSchema<GetsRateByFixAssetMachineryIdResponse>]
    public async Task<IResult> GetsRateByFixAssetMachineryId([FromBody] GetsRateByFixAssetMachineryIdRequest request, CT ct)
    {
        var result = await _logic.GetsRateByFixAssetMachineryId(request, ct);
        return result.GetHttpResponse();
    }
}