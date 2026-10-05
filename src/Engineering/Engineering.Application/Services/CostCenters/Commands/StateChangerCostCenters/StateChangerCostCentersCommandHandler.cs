using Engineering.Application.Abstractions.Data.CostCenters;

namespace Engineering.Application.Services.CostCenters.Commands.StateChangerCostCenters;

public class StateChangerCostCentersCommandHandler : ICommandHandler<StateChangerCostCentersCommand, bool?>
{
    private readonly ILogger<StateChangerCostCentersCommand> _logger;
    private readonly ICostCenterRepository _repository;

    public StateChangerCostCentersCommandHandler(ILogger<StateChangerCostCentersCommand> logger, ICostCenterRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerCostCentersCommand request, CT ct)
    {
        try
        {
            var Ids = request.Items.Select(x => x.Id).ToList();
            var costCenters = await _repository.GetByIdsIncludeType(Ids, ct);
            if (request.State)
                foreach (var item in request.Items)
                {
                    var costCenter = costCenters.FirstOrDefault(x => x.Id == item.Id);
                    if (item.IsActive != request.State)
                    {
                        item.SetActive();
                        costCenter.AddHistory();
                        await _repository.Update(item);
                    }
                }
            else
                foreach (var item in request.Items)
                {
                    var costCenter = costCenters.FirstOrDefault(x => x.Id == item.Id);
                    if (item.IsActive != request.State)
                    {
                        item.SetDeactivate();
                        costCenter.AddHistory();
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