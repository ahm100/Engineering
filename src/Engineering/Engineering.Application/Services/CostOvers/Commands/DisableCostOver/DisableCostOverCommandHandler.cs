using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.DisableCostOver;

public class DisableCostOverCommandHandler : ICommandHandler<DisableCostOverCommand, CostOver>
{
    private readonly ILogger<DisableCostOverCommand> _logger;
    private readonly ICostOverRepository _repository;

    public DisableCostOverCommandHandler(
        ILogger<DisableCostOverCommand> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostOver?>> Handle(DisableCostOverCommand request, CT ct)
    {
        try
        {
            var entity = request.Entity;
            if (entity is null)
                return Result.Failure<CostOver>(CostOverErrors.CostOverWithIdNotFound);
            //if (entity.ContractCostOvers.Any())
            //    return Result.Failure<CostOver>(CostOverErrors.CanNottDeleteBecauseOfContract);
            //if (entity.ContractCostOvers.Any(a => a.ContractCostOverImpacts.Any()))
            //    return Result.Failure<CostOver>(CostOverErrors.CanNottDeleteBecauseOfImpacts);

            entity.SetIsDeleted();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostOver>(SharedErrors.UnknownError);
        }
    }
}