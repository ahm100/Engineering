namespace Engineering.Application.Services.DailyProjectOperations.Models.CreateDailyProjectOperation;

public record CreateDailyProjectOperationExpertModel(long ConsumableVolumeExpertId,
                                                     long ThirdPartyId,
                                                               string FinalValue,
                                                               string? UnusedValue);
