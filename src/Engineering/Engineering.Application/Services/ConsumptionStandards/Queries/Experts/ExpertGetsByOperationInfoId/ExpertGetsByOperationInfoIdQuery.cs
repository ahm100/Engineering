using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Queries.Experts.ExpertGetsByOperationInfoId;

public record ExpertGetsByOperationInfoIdQuery(
    long OprationInfoId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ConsumptionStandardExpert>>>;