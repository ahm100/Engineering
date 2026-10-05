using Engineering.Application.Services.CabinTypes;
using Engineering.Application.Services.CabinTypes.Models.ActiveCabinType;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeCodeCreator;
using Engineering.Application.Services.CabinTypes.Models.CabinTypeGroupDelete;
using Engineering.Application.Services.CabinTypes.Models.CreateCabinType;
using Engineering.Application.Services.CabinTypes.Models.DeleteCabinType;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;
using Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;
using Engineering.Application.Services.CabinTypes.Models.GetsActiveCabinTypes;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinType;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelEnum;
using Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelExporter;
using Engineering.Application.Services.CabinTypes.Models.InactiveCabinType;
using Engineering.Application.Services.CabinTypes.Models.StateChangerCabinTypes;
using Engineering.Application.Services.CabinTypes.Models.UpdateCabinType;

[Authorize]
[Route("api/engineering/v1/CabinType")]
public class CabinTypeController : ControllerBase
{
    private readonly ILogger<CabinTypeController> _logger;
    private readonly ICabinTypeLogic _logic;

    public CabinTypeController(
        ILogger<CabinTypeController> logger,
        ICabinTypeLogic logic) : base()
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("AddCabinType")]
    [ResponseSchema<CreateCabinTypeResponse>]
    public async Task<IResult> AddCabinType(
        [FromBody] CreateCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("AddCabinType");
        var result = await _logic.CreateCabinType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CabinTypeCodeCreator")]
    [ResponseSchema<CabinTypeCodeCreatorResponse>]
    public async Task<IResult> CabinTypeCodeCreator(
        [FromBody] CabinTypeCodeCreatorRequest request, CT ct)
    {
        _logger.LogInformation("CabinTypeCodeCreator");
        var result = await _logic.CabinTypeCodeCreator(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CabinTypeGroupDelete")]
    [ResponseSchema<CabinTypeGroupDeleteResponse>]
    public async Task<IResult> CabinTypeGroupDelete(
        [FromBody] CabinTypeGroupDeleteRequest request, CT ct)
    {
        _logger.LogInformation("CabinTypeGroupDelete");
        var result = await _logic.CabinTypeGroupDelete(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActivateCabinTypes")]
    [ResponseSchema<StateChangerCabinTypesResponse>]
    public async Task<IResult> ActivateCabinTypes(
        [FromBody] ActivateCabinTypesRequest request, CT ct)
    {
        _logger.LogInformation("ActivateCabinTypes");
        var result = await _logic.StateChangerCabinTypes(new(request.Ids, true), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactivateCabinTypes")]
    [ResponseSchema<StateChangerCabinTypesResponse>]
    public async Task<IResult> InactivateCabinTypes(
        [FromBody] InactivateCabinTypesRequest request, CT ct)
    {
        _logger.LogInformation("InactivateCabinTypes");
        var result = await _logic.StateChangerCabinTypes(new(request.Ids, false), ct);
        return result.GetHttpResponse();
    }

    [HttpPut("EditCabinType")]
    [ResponseSchema<UpdateCabinTypeResponse>]
    public async Task<IResult> EditCabinType(
        [FromBody] UpdateCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("EditCabinType");
        var result = await _logic.UpdateCabinType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("ActiveCabinType")]
    [ResponseSchema<ActiveCabinTypeResponse>]
    public async Task<IResult> ActiveCabinType(
        [FromBody] ActiveCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("ActiveCabinType");
        var result = await _logic.ActiveCabinType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("InactiveCabinType")]
    [ResponseSchema<InactiveCabinTypeResponse>]
    public async Task<IResult> InactiveCabinType(
        [FromBody] InactiveCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("InactiveCabinType");
        var result = await _logic.InactiveCabinType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCabinTypeById")]
    [ResponseSchema<GetCabinTypeByIdResponse>]
    public async Task<IResult> GetCabinTypeById(
        [FromQuery] GetCabinTypeByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetCabinTypeById");
        var result = await _logic.GetCabinTypeById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCabinTypeByCode")]
    [ResponseSchema<GetCabinTypeByCodeResponse>]
    public async Task<IResult> GetCabinTypeByCode(
        [FromQuery] GetCabinTypeByCodeRequest request, CT ct)
    {
        _logger.LogInformation("GetCabinTypeByCode");
        var result = await _logic.GetCabinTypeByCode(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCabinTypeByName")]
    [ResponseSchema<GetCabinTypeByNameResponse>]
    public async Task<IResult> GetCabinTypeByName(
        [FromQuery] GetCabinTypeByNameRequest request, CT ct)
    {
        _logger.LogInformation("GetCabinTypeByName");
        var result = await _logic.GetCabinTypeByName(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsActiveCabinType")]
    [ResponseSchema<GetsActiveCabinTypesResponse>]
    public async Task<IResult> GetsActiveCabinType(
        [FromQuery] GetsActiveCabinTypesRequest request, CT ct)
    {
        _logger.LogInformation("GetsActiveCabinType");
        var result = await _logic.GetsActiveCabinTypes(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCabinType")]
    [ResponseSchema<GetsCabinTypeResponse>]
    public async Task<IResult> GetsCabinType(
        [FromQuery] GetsCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("GetsCabinType");
        var result = await _logic.GetsCabinType(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetsCabinTypeExcelEnum")]
    [ResponseSchema<GetsCabinTypeExcelEnumResponse>]
    public async Task<IResult> GetsCabinTypeExcelEnum(
        [FromQuery] GetsCabinTypeExcelEnumRequest request, CT ct)
    {
        _logger.LogInformation("GetsCabinTypeExcelEnum");
        var result = await _logic.GetsCabinTypeExcelEnum(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetsCabinTypeExcelExporter")]
    [ResponseSchema<GetsCabinTypeExcelExporterResponse>]
    public async Task<IResult> GetsCabinTypeExcelExporter(
        [FromBody] GetsCabinTypeExcelExporterRequest request, CT ct)
    {
        _logger.LogInformation("GetsCabinTypeExcelExporter");
        var result = await _logic.GetsCabinTypeExcelExporter(request, ct);
        return result.GetHttpResponse();
    }

    [HttpDelete("DeleteCabinType")]
    [ResponseSchema<DeleteCabinTypeResponse>]
    public async Task<IResult> DeleteCabinType(
        [FromQuery] DeleteCabinTypeRequest request, CT ct)
    {
        _logger.LogInformation("DeleteCabinType");
        var result = await _logic.DeleteCabinType(request, ct);
        return result.GetHttpResponse();
    }
}