using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationExpert;

public record CreateDailyProjectOperationExpertCommand(long ThirdPartyId,
                                                       long FinalValue,
                                                       long? UnusedValue,
                                                       DailyProjectOperation DailyProjectOperation,
                                                       ConsumableVolumeExpert ConsumableVolumeExpert) : ICommand<DailyProjectOperationExpert>;
