using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;

namespace Engineering.Application.Services.ConsumptionStandards.Queries.Machiner.MachineryGetsByOperationInfoId;

public record MachineryGetsByOperationInfoIdQuery(
    long OprationInfoId,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<ConsumptionStandardMachinery>>>;