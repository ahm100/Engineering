using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Domain.Entities.EngineeringConfig;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Commands.CreateCodingConfig;

public class CreateCodingConfigCommandHandler : ICommandHandler<CreateCodingConfigCommand, EngineeringCodingConfig?>
{
    private readonly ILogger<CreateCodingConfigCommandHandler> _logger;
    private readonly IEngineeringConfigRepository _configRepository;
    private readonly IEngineeringCodingConfigRepository _repository;
    private readonly IMediator _mediator;

    public CreateCodingConfigCommandHandler(ILogger<CreateCodingConfigCommandHandler> logger,
        IEngineeringConfigRepository configRepository,
        IEngineeringCodingConfigRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _configRepository = configRepository;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<EngineeringCodingConfig?>> Handle(CreateCodingConfigCommand request, CT ct)
    {
        try
        {
            var config = await _configRepository.GetConfigById(request.ConfigId, ct);
            if (config is null)
                return Result.Failure<EngineeringCodingConfig?>(EngineeringConfigErrors.ConfigWithIdNotFound)!;

            var create = new EngineeringCodingConfig(config,
                request.Type,
                request.Prefix,
                request.IsActive);

            await _repository.Create(create, ct);

            return create;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<EngineeringCodingConfig?>(SharedErrors.UnknownError);
        }
    }
}