using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Queries.Experts.ExpertsByOperationInfoId;

public class ExpertsByOperationInfoIdQueryHandler : IQueryHandler<ExpertsByOperationInfoIdQuery, DataResult<List<ConsumptionStandardExpert>>>
{
    private readonly IConsumptionStandardExpertRepository _repository;
    private readonly ILogger<ExpertsByOperationInfoIdQueryHandler> _logger;

    public ExpertsByOperationInfoIdQueryHandler(ILogger<ExpertsByOperationInfoIdQueryHandler> logger, IConsumptionStandardExpertRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ConsumptionStandardExpert>>?>> Handle(ExpertsByOperationInfoIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.ExpertsByOprationInfoId(request.OprationInfoId, ct);

            return result.Data.Any() ?
                new DataResult<List<ConsumptionStandardExpert>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ConsumptionStandardExpert>>>(ExpertStandardErrors.ExpertsNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ConsumptionStandardExpert>>>(SharedErrors.UnknownError);
        }
    }
}