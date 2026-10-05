using Engineering.Application.Abstractions.Data.MachineTypes;

namespace Engineering.Application.Services.MachineTypes.Commands.StateChangerMachineTypes;

public class StateChangerMachineTypesCommandHandler : ICommandHandler<StateChangerMachineTypesCommand, bool?>
{
    private readonly ILogger<StateChangerMachineTypesCommand> _logger;
    private readonly IMachineTypeRepository _repository;

    public StateChangerMachineTypesCommandHandler(ILogger<StateChangerMachineTypesCommand> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerMachineTypesCommand request, CT ct)
    {
        try
        {
            if (request.State)
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetActive();
                        await _repository.Update(item);
                    }
                }
            else
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetInActive();
                        await _repository.Update(item);
                    }
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}