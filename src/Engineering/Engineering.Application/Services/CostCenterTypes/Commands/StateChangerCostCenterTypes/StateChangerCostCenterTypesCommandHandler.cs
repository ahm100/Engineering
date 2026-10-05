using Engineering.Application.Abstractions.Data.CostCenters;

namespace Engineering.Application.Services.CostCenterTypes.Commands.StateChangerCostCenterTypes;

public class StateChangerCostCenterTypesCommandHandler : ICommandHandler<StateChangerCostCenterTypesCommand, bool?>
{
    private readonly ILogger<StateChangerCostCenterTypesCommand> _logger;
    private readonly ICostCenterTypeRepository _repository;

    public StateChangerCostCenterTypesCommandHandler(
        ILogger<StateChangerCostCenterTypesCommand> logger,
        ICostCenterTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerCostCenterTypesCommand request, CT ct)
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
                        item.SetDeactivate();
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