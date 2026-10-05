using Engineering.Application.Abstractions.Data.Machineries;

namespace Engineering.Application.Services.MachineriesGroups.Commands.MachineriesGroupCodeCreator;

public class MachineriesGroupCodeCreatorCommandHandler : ICommandHandler<MachineriesGroupCodeCreatorCommand, string?>
{
    private readonly ILogger<MachineriesGroupCodeCreatorCommand> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public MachineriesGroupCodeCreatorCommandHandler(ILogger<MachineriesGroupCodeCreatorCommand> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<string?>> Handle(MachineriesGroupCodeCreatorCommand request, CT ct)
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