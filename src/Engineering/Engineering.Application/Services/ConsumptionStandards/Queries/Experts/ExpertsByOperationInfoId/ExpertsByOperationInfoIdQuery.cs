using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Queries.Experts.ExpertsByOperationInfoId;

public record ExpertsByOperationInfoIdQuery(
    long OprationInfoId
    ) : IQuery<DataResult<List<ConsumptionStandardExpert>>>;