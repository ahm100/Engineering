using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.UpdateCodingConfig;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Commands.UpdateCodingConfig;

public class UpdateCodingConfigCommandHandler : ICommandHandler<UpdateCodingConfigCommand, UpdateCodingConfigResponse?>
{
    private readonly ILogger<UpdateCodingConfigCommandHandler> _logger;
    private readonly IEngineeringConfigRepository _configRepository;
    private readonly IEngineeringCodingConfigRepository _repository;
    private readonly IMediator _mediator;

    public UpdateCodingConfigCommandHandler(ILogger<UpdateCodingConfigCommandHandler> logger,
        IEngineeringConfigRepository configRepository,
        IEngineeringCodingConfigRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _configRepository = configRepository;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<UpdateCodingConfigResponse?>> Handle(UpdateCodingConfigCommand request, CT ct)
    {
        try
        {
            var config = await _configRepository.GetConfigById(request.ConfigId, ct);
            if (config is null)
                return Result.Failure<UpdateCodingConfigResponse?>(EngineeringConfigErrors.ConfigWithIdNotFound)!;

            var codeConfig = await _repository.FindById(request.Id, ct);
            if (codeConfig is null)
                return Result.Failure<UpdateCodingConfigResponse?>(EngineeringConfigErrors.CodeConfigWithIdNotFound)!;

            codeConfig.Update(config,
                request.Type,
                request.Prefix,
                request.IsActive);

            await _repository.Update(codeConfig);

            return new UpdateCodingConfigResponse(codeConfig.Id, true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateCodingConfigResponse?>(SharedErrors.UnknownError);
        }
    }
}