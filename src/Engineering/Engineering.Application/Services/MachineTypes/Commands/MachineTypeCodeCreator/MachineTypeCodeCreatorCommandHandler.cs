using Engineering.Application.Abstractions.Data.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Commands.MachineTypeCodeCreator;

public class MachineTypeCodeCreatorCommandHandler : ICommandHandler<MachineTypeCodeCreatorCommand, string?>
{
    private readonly ILogger<MachineTypeCodeCreatorCommand> _logger;
    private readonly IMachineTypeRepository _repository;

    public MachineTypeCodeCreatorCommandHandler(ILogger<MachineTypeCodeCreatorCommand> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(MachineTypeCodeCreatorCommand request, CT ct)
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