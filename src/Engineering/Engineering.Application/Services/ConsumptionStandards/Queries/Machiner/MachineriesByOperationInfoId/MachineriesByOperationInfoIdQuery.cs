using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;

namespace Engineering.Application.Services.ConsumptionStandards.Queries.Machiner.MachineriesByOperationInfoId;

public record MachineriesByOperationInfoIdQuery(
    long OprationInfoId
    ) : IQuery<DataResult<List<ConsumptionStandardMachinery>>>;