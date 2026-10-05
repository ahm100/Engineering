using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.DisableExpert;

public class DisableExpertCommandHandler : ICommandHandler<DisableExpertCommand, ConsumptionStandardExpert>
{
    private readonly ILogger<DisableExpertCommand> _logger;
    private readonly IConsumptionStandardExpertRepository _repository;

    public DisableExpertCommandHandler(ILogger<DisableExpertCommand> logger, IConsumptionStandardExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardExpert?>> Handle(DisableExpertCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.ExpertId, ct);
            if (entity is null)
                return Result.Failure<ConsumptionStandardExpert>(ExpertStandardErrors.ExpertWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<ConsumptionStandardExpert>(ExpertStandardErrors.IsDeleted);

            entity.SetIsDeleted();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumptionStandardExpert>(SharedErrors.UnknownError);
        }
    }
}