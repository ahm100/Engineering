namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;

public record UpdateDailyProjectOperationProductModel(long ConsumableVolumeProductId,
                                                      long ProductId,
                                                      decimal FinalValue,
                                                      decimal? UnusedValue);
