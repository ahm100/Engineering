using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.ActiveCostOver;

public class ActiveCostOverCommandHandler : ICommandHandler<ActiveCostOverCommand, CostOver>
{
    private readonly ILogger<ActiveCostOverCommand> _logger;
    private readonly ICostOverRepository _repository;

    public ActiveCostOverCommandHandler(
        ILogger<ActiveCostOverCommand> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostOver?>> Handle(ActiveCostOverCommand request, CT ct)
    {
        try
        {
            var CostOverEntity = request.Entity;
            if (CostOverEntity is null)
                return Result.Failure<CostOver>(CostOverErrors.CostOverWithIdNotFound);
            if (CostOverEntity.IsActive)
                return Result.Failure<CostOver>(CostOverErrors.IsActive);

            CostOverEntity.SetActive();
            await _repository.Update(CostOverEntity);
            return CostOverEntity;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CostOver>(SharedErrors.UnknownError);
        }
    }
}