using Engineering.Domain.Entities.DailyProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.RequestMachineries;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.CreateDailyProjectOperationMachinery;

public record CreateDailyProjectOperationMachineryCommand(RequestMachinery RequestMachinery,
                                                       long? FinalValue,
                                                       long? UnusedValue,
                                                       decimal? Number,
                                                       DailyProjectOperation DailyProjectOperation,
                                                       ConsumableVolumeMachinery ConsumableVolumeMachinery) : ICommand<DailyProjectOperationMachinery>;
