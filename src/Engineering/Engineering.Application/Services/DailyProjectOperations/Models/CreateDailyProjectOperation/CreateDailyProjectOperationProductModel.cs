namespace Engineering.Application.Services.DailyProjectOperations.Models.CreateDailyProjectOperation;

public record CreateDailyProjectOperationProductModel(long ConsumableVolumeProductId,
                                                      long ProductId,
                                                      decimal FinalValue,
                                                      decimal? UnusedValue);
