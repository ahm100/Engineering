using Engineering.Application.Abstractions.Data.MachineTypes;

namespace Engineering.Application.Services.CabinTypes.Commands.StateChangerCabinTypes;

public class StateChangerCabinTypesCommandHandler : ICommandHandler<StateChangerCabinTypesCommand, bool?>
{
    private readonly ILogger<StateChangerCabinTypesCommand> _logger;
    private readonly ICabinTypeRepository _repository;

    public StateChangerCabinTypesCommandHandler(
        ILogger<StateChangerCabinTypesCommand> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerCabinTypesCommand request, CT ct)
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