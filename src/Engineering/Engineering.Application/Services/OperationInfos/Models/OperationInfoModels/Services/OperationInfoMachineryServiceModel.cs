namespace Engineering.Application.Services.OperationInfos.Models.OperationInfoModels.Services;
using MachineryEntity = Engineering.Domain.Entities.Machineries.Machinery;

public record OperationInfoMachineryServiceModel(
    MachineryEntity Machinery,
    int MachineryNumber,
    long TimeSpant,
    decimal? UnusedPercentage
 );
