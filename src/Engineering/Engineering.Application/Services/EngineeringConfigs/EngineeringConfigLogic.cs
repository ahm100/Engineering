using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Commands.CreateCodingConfig;
using Engineering.Application.Services.EngineeringConfigs.Commands.DeleteCodingConfig;
using Engineering.Application.Services.EngineeringConfigs.Commands.UpdateCodingConfig;
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
using Engineering.Application.Services.EngineeringConfigs.Queries.GetCodingConfigByEngConfigId;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetCodingConfigById;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetConfigHistoryByConfigId;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetFltrCodingConfigs;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs;

public partial class EngineeringConfigLogic : IEngineeringConfigLogic
{
    private readonly IMediator _mediator;
    private readonly ILogger<EngineeringConfigLogic> _logger;
    private readonly IUserInfoService _userInfoService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEngineeringConfigRepository _repo;

    public EngineeringConfigLogic(
        IMediator mediator,
        ILogger<EngineeringConfigLogic> logger,
        IUnitOfWork unitOfWork,
        IUserInfoService userInfoService,
        IEngineeringConfigRepository repo)
    {
        _mediator = mediator;
        _logger = logger;
        _userInfoService = userInfoService;
        _unitOfWork = unitOfWork;
        _repo = repo;
    }

    public async Task<Result<CreateConfigResponse?>> CreateConfig(
         CreateConfigRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<CreateConfigResponse>(GlobalErrors.InvalidCompany);

        var create = await CreateConfigHandle(request, companyId.Value!, ct);
        if (create.IsBad())
            return create.Failure<CreateConfigResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateConfigResponse(create.Value.Id, true);
    }

    public async Task<Result<CreateCodingConfigResponse?>> CreateCodingConfig(
         CreateCodingConfigRequest request, CT ct)
    {
        var create = await _mediator.Send(new CreateCodingConfigCommand(request.ConfigId,
            request.Type,
            request.Prefix,
            request.IsActive), ct);
        if (create.IsBad())
            return create.Failure<CreateCodingConfigResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new CreateCodingConfigResponse(create.Value.Id, true);
    }

    public async Task<Result<UpdateConfigResponse?>> UpdateConfig(
         UpdateConfigRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<UpdateConfigResponse>(GlobalErrors.InvalidCompany);

        var config = await _repo.GetConfigById(request.Id, ct);
        if (config is null)
            return Result.Failure<UpdateConfigResponse>(EngineeringConfigErrors.ConfigWithIdNotFound);
        var update = await UpdateConfigHandle(config, request, ct);
        if (update.IsBad())
            return update.Failure<UpdateConfigResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new UpdateConfigResponse(true);
    }

    public async Task<Result<UpdateCodingConfigResponse?>> UpdateCodingConfig(
         UpdateCodingConfigRequest request, CT ct)
    {
        var update = await _mediator.Send(new UpdateCodingConfigCommand(request.Id,
            request.ConfigId,
            request.Type,
            request.Prefix,
            request.IsActive), ct);
        if (update.IsBad())
            return update.Failure<UpdateCodingConfigResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return update;
    }

    public async Task<Result<DeleteConfigResponse?>> DeleteConfig(
         DeleteConfigRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<DeleteConfigResponse>(GlobalErrors.InvalidCompany);

        var config = await _repo.GetConfigById(request.Id, ct);
        if (config is null)
            return Result.Failure<DeleteConfigResponse>(EngineeringConfigErrors.ConfigWithIdNotFound);
        var update = await DeleteConfigHandle(config, ct);
        if (update.IsBad())
            return update.Failure<DeleteConfigResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return new DeleteConfigResponse(true);
    }

    public async Task<Result<DeleteCodingConfigResponse?>> DeleteCodingConfig(
         DeleteCodingConfigRequest request, CT ct)
    {
        var update = await _mediator.Send(new DeleteCodingConfigCommand(request.Id), ct);
        if (update.IsBad())
            return update.Failure<DeleteCodingConfigResponse>()!;

        await _unitOfWork.CommitAsync(ct);
        return update;
    }

    public async Task<Result<GetFltrConfigsResponse?>> GetFltrConfigs(
         GetFltrConfigsRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetFltrConfigsResponse>(GlobalErrors.InvalidCompany);

        var configs = await GetFltrConfigsHandle(request, companyId.Value!, ct);
        if (configs.IsBad())
            return configs.Failure<GetFltrConfigsResponse>()!;

        return configs;
    }

    public async Task<Result<GetFltrCodingConfigsResponse?>> GetFltrCodingConfigs(
         GetFltrCodingConfigsRequest request, CT ct)
    {
        var configs = await _mediator.Send(new GetFltrCodingConfigsQuery(request.IsActive,
            request.PageIndex,
            request.PageSize), ct);
        if (configs.IsBad())
            return configs.Failure<GetFltrCodingConfigsResponse>()!;

        return configs;
    }

    public async Task<Result<GetActiveConfigResponse?>> GetActiveConfig(
         GetActiveConfigRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetActiveConfigResponse>(GlobalErrors.InvalidCompany);

        var config = await GetActiveConfigHandle(request, companyId.Value!, ct);
        if (config.IsBad())
            return config.Failure<GetActiveConfigResponse>()!;

        return config;
    }

    public async Task<Result<GetConfigByIdResponse?>> GetConfigById(
         GetConfigByIdRequest request, CT ct)
    {
        var companyId = CompanyValidator.GetCompanyId(_userInfoService);
        if (await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
            return Result.Failure<GetConfigByIdResponse>(GlobalErrors.InvalidCompany);

        var config = await GetConfigByIdHandle(request, ct);
        if (config.IsBad())
            return config.Failure<GetConfigByIdResponse>()!;

        return config;
    }

    public async Task<Result<GetCodingConfigByIdResponse?>> GetCodingConfigById(
         GetCodingConfigByIdRequest request, CT ct)
    {
        var codingConfigs = await _mediator.Send(new GetCodingConfigByIdQuery(request.Id), ct);
        if (codingConfigs.IsBad())
            return codingConfigs.Failure<GetCodingConfigByIdResponse>()!;

        return codingConfigs;
    }

    public async Task<Result<GetConfigHistoryByConfigIdResponse?>> GetConfigHistoryByConfigId(
         GetConfigHistoryByConfigIdRequest request, CT ct)
    {
        var result = await _mediator.Send(new GetConfigHistoryByConfigIdQuery(request.ConfigId, request.PageIndex, request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetConfigHistoryByConfigIdResponse?>();

        return result;
    }

    public async Task<Result<GetCodingConfigByEngConfigIdResponse?>> GetCodingConfigByEngConfigId(
         GetCodingConfigByEngConfigIdRequest request, CT ct)
    {
        var result = await _mediator.Send(new GetCodingConfigByEngConfigIdQuery(request.ConfigId, request.PageIndex, request.PageSize), ct);
        if (result.IsBad())
            return result.Failure<GetCodingConfigByEngConfigIdResponse?>();

        return result;
    }
}