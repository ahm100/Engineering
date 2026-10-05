namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;

public record UpdateDailyProjectOperationExpertModel(long ConsumableVolumeExpertId,
                                                     long ThirdPartyId,
                                                               string FinalValue,
                                                               string? UnusedValue);
