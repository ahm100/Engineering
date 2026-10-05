namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;

public record UpdateDailyProjectOperationMachineryModel(long ConsumableVolumeMachineryId,
                                                        long RequestMachineryId,
                                                        string? FinalValue,
                                                        string? UnusedValue,
                                                        decimal? Number
                                                        );
