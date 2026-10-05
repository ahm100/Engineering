using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.UpdateExpert;

public class UpdateExpertCommandHandler : ICommandHandler<UpdateExpertCommand, ConsumptionStandardExpert>
{
    private readonly ILogger<UpdateExpertCommand> _logger;
    private readonly IConsumptionStandardExpertRepository _repository;

    public UpdateExpertCommandHandler(ILogger<UpdateExpertCommand> logger, IConsumptionStandardExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardExpert?>> Handle(UpdateExpertCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ConsumptionStandardExpert>(ExpertStandardErrors.ExpertWithIdNotFound);

            entity.SetExpertUnitId(request.ExpertUnitId);
            entity.SetExpertNumber(request.ExpertNumber);
            entity.SetUnusedPercentage(request.UnusedPercentage);
            entity.SetTimeSpant(request.TimeSpant);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ConsumptionStandardExpert>(SharedErrors.UnknownError);
        }
    }
}