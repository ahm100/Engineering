using Engineering.Domain.Entities.OperationInfos;
using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.CreateExpert;

public record CreateExpertCommand(
    OperationInfo OperationInfo,
    long ExpertUnitId,
    int ExpertNumber,
    long TimeSpant,
    decimal? UnusedPercentage
    ) : ICommand<ConsumptionStandardExpert>;