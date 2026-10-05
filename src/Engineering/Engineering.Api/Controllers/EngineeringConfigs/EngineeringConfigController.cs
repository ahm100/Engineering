using Engineering.Api.Extensions.Enums;
using Engineering.Application.Services.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.CreateCodingConfig;
using Engineering.Application.Services.EngineeringConfigs.Contracts.CreateConfig;
using Engineering.Application.Services.EngineeringConfigs.Contracts.DeleteCodingConfig;
using Engineering.Application.Services.EngineeringConfigs.Contracts.DeleteConfig;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetActiveConfig;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigByEngConfigId;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetCodingConfigById;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigById;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigHistoryByConfigId;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrCodingConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateCodingConfig;
using Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateConfig;
using Engineering.Domain.Entities.EngineeringConfig.Enum;
using System.ComponentModel;

namespace Engineering.Api.Controllers.EngineeringConfigs;

[ApiController]
[Route("api/engineering/v1/EngineeringConfig")]
public class EngineeringConfigController : ControllerBase
{
    private readonly IEngineeringConfigLogic _logic;

    public EngineeringConfigController(IEngineeringConfigLogic logic)
    {
        _logic = logic;
    }

    [HttpPost("CreateConfig")]
    [ResponseSchema<CreateConfigResponse>]
    public async Task<IResult> CreateConfig(
        [FromBody] CreateConfigRequest request, CT ct)
    {
        var result = await _logic.CreateConfig(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("CreateCodingConfig")]
    [ResponseSchema<CreateCodingConfigResponse>]
    public async Task<IResult> CreateCodingConfig(
        [FromBody] CreateCodingConfigRequest request, CT ct)
    {
        var result = await _logic.CreateCodingConfig(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateConfig")]
    [ResponseSchema<UpdateConfigResponse>]
    public async Task<IResult> UpdateConfig(
        [FromBody] UpdateConfigRequest request, CT ct)
    {
        var result = await _logic.UpdateConfig(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPut("UpdateCodingConfig")]
    [ResponseSchema<UpdateCodingConfigResponse>]
    public async Task<IResult> UpdateCodingConfig(
        [FromBody] UpdateCodingConfigRequest request, CT ct)
    {
        var result = await _logic.UpdateCodingConfig(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteConfig")]
    [ResponseSchema<DeleteConfigResponse>]
    public async Task<IResult> DeleteConfig(
        [FromBody] DeleteConfigRequest request, CT ct)
    {
        var result = await _logic.DeleteConfig(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("DeleteCodingConfig")]
    [ResponseSchema<DeleteCodingConfigResponse>]
    public async Task<IResult> DeleteCodingConfig(
        [FromBody] DeleteCodingConfigRequest request, CT ct)
    {
        var result = await _logic.DeleteCodingConfig(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrConfigs")]
    [ResponseSchema<GetFltrConfigsResponse>]
    public async Task<IResult> GetFltrConfigs(
        [FromBody] GetFltrConfigsRequest request, CT ct)
    {
        var result = await _logic.GetFltrConfigs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetFltrCodingConfigs")]
    [ResponseSchema<GetFltrCodingConfigsResponse>]
    public async Task<IResult> GetFltrCodingConfigs(
        [FromBody] GetFltrCodingConfigsRequest request, CT ct)
    {
        var result = await _logic.GetFltrCodingConfigs(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetActiveConfig")]
    [ResponseSchema<GetActiveConfigResponse>]
    public async Task<IResult> GetActiveConfig(
        [FromQuery] GetActiveConfigRequest request, CT ct)
    {
        var result = await _logic.GetActiveConfig(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetConfigById")]
    [ResponseSchema<GetConfigByIdResponse>]
    public async Task<IResult> GetConfigById(
        [FromQuery] GetConfigByIdRequest request, CT ct)
    {
        var result = await _logic.GetConfigById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCodingConfigById")]
    [ResponseSchema<GetCodingConfigByIdResponse>]
    public async Task<IResult> GetCodingConfigById(
        [FromQuery] GetCodingConfigByIdRequest request, CT ct)
    {
        var result = await _logic.GetCodingConfigById(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("config-history-by-config-id")]
    [ResponseSchema<GetConfigHistoryByConfigIdResponse>]
    public async Task<IResult> GetConfigHistoryByConfigId(
        [FromQuery] GetConfigHistoryByConfigIdRequest request, CT ct)
    {
        var result = await _logic.GetConfigHistoryByConfigId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpGet("GetCodingConfigByEngConfigId")]
    [ResponseSchema<GetCodingConfigByEngConfigIdResponse>]
    public async Task<IResult> GetCodingConfigByEngConfigId(
        [FromQuery] GetCodingConfigByEngConfigIdRequest request, CT ct)
    {
        var result = await _logic.GetCodingConfigByEngConfigId(request, ct);
        return result.GetHttpResponse();
    }

    [HttpPost("GetCodingAlgorithmType")]
    [Description("Get enum values for CodingAlgorithmType.")]
    [ResponseSchema<GetEnumsResponse>]
    public IResult GetCodingAlgorithmType(
        [FromBody] GetEnumsRequest request, CT ct)
    {
        var result = EnumExtensions.GetEnums<CodingAlgorithmType>(request);
        return Result.Success<GetEnumsResponse>(result).GetHttpResponse();
    }

}