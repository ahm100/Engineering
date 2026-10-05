using Engineering.Application.Abstractions.Data.EngineeringConfigs;
using Engineering.Application.Services.EngineeringConfigs.Contracts.DeleteCodingConfig;
using Engineering.Domain.Errors.EngineeringConfigs;

namespace Engineering.Application.Services.EngineeringConfigs.Commands.DeleteCodingConfig;

public class DeleteCodingConfigCommandHandler : ICommandHandler<DeleteCodingConfigCommand, DeleteCodingConfigResponse?>
{
    private readonly ILogger<DeleteCodingConfigCommandHandler> _logger;
    private readonly IEngineeringConfigRepository _configRepository;
    private readonly IEngineeringCodingConfigRepository _repository;
    private readonly IMediator _mediator;

    public DeleteCodingConfigCommandHandler(ILogger<DeleteCodingConfigCommandHandler> logger,
        IEngineeringConfigRepository configRepository,
        IEngineeringCodingConfigRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _configRepository = configRepository;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<DeleteCodingConfigResponse?>> Handle(DeleteCodingConfigCommand request, CT ct)
    {
        try
        {
            var codeConfig = await _repository.FindById(request.Id, ct);
            if (codeConfig is null)
                return Result.Failure<DeleteCodingConfigResponse?>(EngineeringConfigErrors.CodeConfigWithIdNotFound)!;

            codeConfig.SoftDelete();

            await _repository.Update(codeConfig);

            return new DeleteCodingConfigResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DeleteCodingConfigResponse?>(SharedErrors.UnknownError);
        }
    }
}