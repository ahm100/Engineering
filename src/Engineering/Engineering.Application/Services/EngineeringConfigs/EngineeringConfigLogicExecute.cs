using Engineering.Application.Services.EngineeringConfigs.Contracts.CreateConfig;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetActiveConfig;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetConfigById;
using Engineering.Application.Services.EngineeringConfigs.Contracts.GetFltrConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateConfig;
using Engineering.Domain.Entities.EngineeringConfig;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs;

public partial class EngineeringConfigLogic
{
    private async Task<Result<EngineeringConfig?>> CreateConfigHandle(
         CreateConfigRequest request, long companyId, CT ct)
    {
        try
        {
            if (request.IsActive != null && request.IsActive.Value)
            {
                var configs = await _repo.GetActiveEngineeringConfigs(companyId, ct);
                foreach (var item in configs)
                    item.SetDeactivate();
            }

            var entity = await _repo.Create(new EngineeringConfig(
                companyId,
                request.SendTelegramMessage,
                request.ProjectThirdParties,
                request.IsActive), ct);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EngineeringConfig>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<EngineeringConfig?>> UpdateConfigHandle(
         EngineeringConfig entity, UpdateConfigRequest request, CT ct)
    {
        try
        {
            if (request.IsActive != null && request.IsActive.Value)
            {
                var configs = await _repo.GetActiveEngineeringConfigs(entity.CompanyId, ct);
                foreach (var item in configs)
                    item.SetDeactivate();
            }
            entity.Update(request.SendTelegramMessage, request.ProjectThirdParties, request.IsActive);

            await _repo.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EngineeringConfig?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<bool>> DeleteConfigHandle(
         EngineeringConfig entity, CT ct)
    {
        try
        {
            entity.SoftDelete();

            await _repo.Update(entity);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }


    private async Task<Result<List<EngineeringConfig>?>> GetActiveEngineeringConfigsHandle(
       long companyId, CT ct)
    {
        try
        {
            var configs = await _repo.GetActiveEngineeringConfigs(companyId, ct);
            return configs ?? Result.Failure<List<EngineeringConfig>?>(EngineeringConfigErrors.ActiveConfigNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<EngineeringConfig>?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetFltrConfigsResponse?>> GetFltrConfigsHandle(
       GetFltrConfigsRequest request, long companyId, CT ct)
    {
        try
        {
            var configs = await _repo.GetFltrConfigs(companyId, request.SendTelegramMessage, request.IsActive, request.PageIndex, request.PageSize, ct);

            return configs.Data.Any() ?
                new GetFltrConfigsResponse
                (
                    configs.Data,
                    configs.RowCount
                )
                : Result.Failure<GetFltrConfigsResponse?>(EngineeringConfigErrors.FilteredConfigNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrConfigsResponse?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetActiveConfigResponse?>> GetActiveConfigHandle(
       GetActiveConfigRequest request, long companyId, CT ct)
    {
        try
        {
            var config = await _repo.GetActiveConfig(companyId, ct);

            return config is not null ?
                new GetActiveConfigResponse
                {
                    CompanyId = config.CompanyId,
                    IsActive = config.IsActive,
                    SendTelegramMessage = config.SendTelegramMessage,
                    ProjectThirdParties = config.ProjectThirdParties,
                    CreatorId = config.CreatorId,
                    Created = config.Created,
                    UpdaterId = config.UpdaterId,
                    Updated = config.Updated,
                } :
                Result.Failure<GetActiveConfigResponse>(EngineeringConfigErrors.ActiveConfigNotFound);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetActiveConfigResponse?>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<GetConfigByIdResponse?>> GetConfigByIdHandle(
       GetConfigByIdRequest request, CT ct)
    {
        try
        {
            var config = await _repo.GetConfigById(request.Id, ct);

            return config is not null ?
                new GetConfigByIdResponse
                {
                    Id = config.Id,
                    CompanyId = config.CompanyId,
                    SendTelegramMessage = config.SendTelegramMessage,
                    ProjectThirdParties = config.ProjectThirdParties,
                    IsActive = config.IsActive,
                    CreatorId = config.CreatorId,
                    Created = config.Created,
                    Updated = config.Updated,
                }
                : Result.Failure<GetConfigByIdResponse?>(EngineeringConfigErrors.FilteredConfigNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetConfigByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}