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

namespace Engineering.Application.Services.EngineeringConfigs;

public interface IEngineeringConfigLogic
{
    Task<Result<CreateConfigResponse?>> CreateConfig(
         CreateConfigRequest request, CT ct);

    Task<Result<CreateCodingConfigResponse?>> CreateCodingConfig(
         CreateCodingConfigRequest request, CT ct);

    Task<Result<UpdateConfigResponse?>> UpdateConfig(
         UpdateConfigRequest request, CT ct);

    Task<Result<UpdateCodingConfigResponse?>> UpdateCodingConfig(
         UpdateCodingConfigRequest request, CT ct);

    Task<Result<DeleteConfigResponse?>> DeleteConfig(
         DeleteConfigRequest request, CT ct);

    Task<Result<DeleteCodingConfigResponse?>> DeleteCodingConfig(
         DeleteCodingConfigRequest request, CT ct);

    Task<Result<GetFltrConfigsResponse?>> GetFltrConfigs(
         GetFltrConfigsRequest request, CT ct);

    Task<Result<GetFltrCodingConfigsResponse?>> GetFltrCodingConfigs(
         GetFltrCodingConfigsRequest request, CT ct);

    Task<Result<GetActiveConfigResponse?>> GetActiveConfig(
         GetActiveConfigRequest request, CT ct);

    Task<Result<GetConfigByIdResponse?>> GetConfigById(
         GetConfigByIdRequest request, CT ct);

    Task<Result<GetCodingConfigByIdResponse?>> GetCodingConfigById(
         GetCodingConfigByIdRequest request, CT ct);

    Task<Result<GetConfigHistoryByConfigIdResponse?>> GetConfigHistoryByConfigId(
         GetConfigHistoryByConfigIdRequest request, CT ct);

    Task<Result<GetCodingConfigByEngConfigIdResponse?>> GetCodingConfigByEngConfigId(
         GetCodingConfigByEngConfigIdRequest request, CT ct);
}