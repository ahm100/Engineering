namespace Engineering.Application.Services.DailyProjectOperations.Models.CreateDailyProjectOperation;

public record CreateDailyProjectOperationMachineryModel(long ConsumableVolumeMachineryId,
                                                        long RequestMachineryId,
                                                        string? FinalValue,
                                                        string? UnusedValue,
                                                        decimal? Number
                                                        );
