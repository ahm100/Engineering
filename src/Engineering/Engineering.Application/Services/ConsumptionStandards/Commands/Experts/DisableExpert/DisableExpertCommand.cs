using ConsumptionStandardExpert = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardExpert;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Experts.DisableExpert;

public record DisableExpertCommand(
    long ExpertId
    ) : ICommand<ConsumptionStandardExpert>;