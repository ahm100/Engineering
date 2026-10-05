using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.UpdateExpert;

public record UpdateExpertCommand(
    long Id,
    long ExpertUnitId,
    int ExpertNumber,
    long TimeSpant,
    decimal? UnusedPercentage
    ) : ICommand<ConsumptionStandardExpert>;