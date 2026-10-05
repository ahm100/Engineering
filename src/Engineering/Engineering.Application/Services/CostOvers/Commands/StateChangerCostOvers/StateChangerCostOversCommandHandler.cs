using Engineering.Application.Abstractions.Data.CostOvers;

namespace Engineering.Application.Services.CostOvers.Commands.StateChangerCostOvers;

public class StateChangerCostOversCommandHandler : ICommandHandler<StateChangerCostOversCommand, bool?>
{
    private readonly ILogger<StateChangerCostOversCommand> _logger;
    private readonly ICostOverRepository _repository;

    public StateChangerCostOversCommandHandler(
        ILogger<StateChangerCostOversCommand> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerCostOversCommand request, CT ct)
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