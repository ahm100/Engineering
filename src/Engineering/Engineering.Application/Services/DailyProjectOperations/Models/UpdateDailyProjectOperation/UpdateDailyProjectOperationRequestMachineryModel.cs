namespace Engineering.Application.Services.DailyProjectOperations.Models.UpdateDailyProjectOperation;

public record UpdateDailyProjectOperationRequestMachineryModel(long RequestMachineryId,
                                                               string FinalValue,
                                                               string? UnusedValue);
