using Engineering.Api.Extensions.Enums;
using Engineering.Application.Extensions;
using Engineering.Application.Services.ProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Contracts.CreateProcesVerbal;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;
using Engineering.Application.Services.ProcesVerbal.Contracts.RemoveProcesVerbal;
using Engineering.Domain.Entities.ProcesVerbal.Enums;
using Gita.Backend.Shared.Domain.Errors;
using System.ComponentModel;

namespace Engineering.Api.Controllers.ProcesVerbals;

[ApiController]
[Route("api/engineering/v1/procesverbal")]
public class ProcesVerbalController : ControllerBase
{
    private readonly IProcesVerbalLogic _logic;
    private readonly ILogger<ProcesVerbalController> _logger;

    public ProcesVerbalController(
        IProcesVerbalLogic logic,
        ILogger<ProcesVerbalController> logger)
    {
        _logger = logger;
        _logic = logic;
    }

    [HttpPost("GetProcesVerbalTypes")]
    [Description("GetProcesVerbalTypes Enum")]
    [ResponseSchema<GetEnumsResponse>]
    public async Task<IResult> GetProcesVerbalTypes(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        _logger.LogInformation($"GetProcesVerbalTypes");
        var result = EnumExtensions.GetEnums<ProcesVerbalType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

    [HttpPost("CreateProcesVerbal")]
    [Description("Insert new proces verbal")]
    [ResponseSchema<CreateProcesVerbalResponse>]
    public async Task<IResult> CreateProcesVerbal(
        [FromBody] CreateProcesVerbalRequest request, CT ct)
    {
        _logger.LogInformation("CreateProcesVerbal");
        var result = await _logic.CreateProcesVerbal(request, ct);

        if (result.IsBad() || result is null)
            return Result.Failure<CreateProcesVerbalResponse?>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

    [HttpDelete("RemoveProcesVerbal")]
    [Description("Remove ProcesVerbal")]
    [ResponseSchema<RemoveProcesVerbalResponse>]
    public async Task<IResult> RemoveProcesVerbal(
        [FromQuery] RemoveProcesVerbalRequest request, CT ct)
    {
        _logger.LogInformation("RemoveProcesVerbal");
        var result = await _logic.RemoveProcesVerbal(request, ct);

        if (result.IsBad() || result is null)
            return Result.Failure<RemoveProcesVerbalResponse>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

    [HttpGet("GetProcesVerbals")]
    [Description("Get ProcesVerbals")]
    [ResponseSchema<GetProcesVerbalsResponse>]
    public async Task<IResult> GetProcesVerbals(
        [FromQuery] GetProcesVerbalsRequest request, CT ct)
    {
        _logger.LogInformation("GetProcesVerbals");
        var result = await _logic.GetProcesVerbals(request, ct);

        if (result.IsBad() || result is null)
            return Result.Failure<GetProcesVerbalsResponse?>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

    [HttpGet("GetProcesVerbalDetailById")]
    [Description("Get ProcesVerbalDetailById")]
    [ResponseSchema<GetProcesVerbalDetailByIdResponse>]
    public async Task<IResult> GetProcesVerbalDetailById(
        [FromQuery] GetProcesVerbalDetailByIdRequest request, CT ct)
    {
        _logger.LogInformation("GetProcesVerbalDetailById");
        var result = await _logic.GetProcesVerbalDetailById(request, ct);

        if (result.IsBad() || result is null)
            return Result.Failure<GetProcesVerbalDetailByIdResponse?>(SharedErrors.UnknownError).GetHttpResponse();

        return result.GetHttpResponse();
    }

}
