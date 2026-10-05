using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.CreateExpert;

public class CreateExpertCommandHandler : ICommandHandler<CreateExpertCommand, ConsumptionStandardExpert>
{
    private readonly ILogger<CreateExpertCommand> _logger;
    private readonly IConsumptionStandardExpertRepository _repository;

    public CreateExpertCommandHandler(ILogger<CreateExpertCommand> logger, IConsumptionStandardExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardExpert?>> Handle(CreateExpertCommand request, CT ct)
    {
        try
        {
            var entity = new ConsumptionStandardExpert(request.OperationInfo, request.ExpertUnitId, request.ExpertNumber,
                request.TimeSpant, request.UnusedPercentage);
            var result = await _repository.Create(entity, ct);
            request.OperationInfo.SetStandard();

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumptionStandardExpert>(SharedErrors.UnknownError);
        }
    }
}