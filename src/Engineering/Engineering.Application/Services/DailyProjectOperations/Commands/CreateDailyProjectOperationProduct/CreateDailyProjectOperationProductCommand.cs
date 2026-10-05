using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationProduct;

public record CreateDailyProjectOperationProductCommand(long ProductId,
                                                        decimal FinalValue,
                                                        decimal? UnusedPercentage,
                                                        DailyProjectOperation DailyProjectOperation,
                                                        ConsumableVolumeProduct ConsumableVolumeProduct) : ICommand<DailyProjectOperationProduct>;
