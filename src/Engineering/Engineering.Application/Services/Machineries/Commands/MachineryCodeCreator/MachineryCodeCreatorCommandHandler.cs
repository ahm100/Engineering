using Engineering.Application.Abstractions.Data.Machineries;

namespace Engineering.Application.Services.Machineries.Commands.MachineryCodeCreator;

public class MachineryCodeCreatorCommandHandler : ICommandHandler<MachineryCodeCreatorCommand, string?>
{
    private readonly ILogger<MachineryCodeCreatorCommand> _logger;
    private readonly IMachineryRepository _repository;

    public MachineryCodeCreatorCommandHandler(ILogger<MachineryCodeCreatorCommand> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(MachineryCodeCreatorCommand request, CT ct)
    {
        try
        {
            var result = await _repository.CodeCreator(request.CompanyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<string>(SharedErrors.UnknownError);
        }
    }
}