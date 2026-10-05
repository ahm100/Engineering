using Engineering.Application.Abstractions.Data.CostOvers;
using CostOver = Engineering.Domain.Entities.CostOvers.CostOver;

namespace Engineering.Application.Services.CostOvers.Commands.InactiveCostOver;

public class InactiveCostOverCommandHandler : ICommandHandler<InactiveCostOverCommand, CostOver>
{
    private readonly ILogger<InactiveCostOverCommand> _logger;
    private readonly ICostOverRepository _repository;

    public InactiveCostOverCommandHandler(
        ILogger<InactiveCostOverCommand> logger,
        ICostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CostOver?>> Handle(InactiveCostOverCommand request, CT ct)
    {
        try
        {
            var CostOverEntity = request.Entity;
            if (CostOverEntity is null)
                return Result.Failure<CostOver>(CostOverErrors.CostOverWithIdNotFound);
            if (CostOverEntity.IsActive == false)
                return Result.Failure<CostOver>(CostOverErrors.IsInactive);

            CostOverEntity.SetInActive();
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