using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachineryRate;

public record UpdateFixAssetMachineryRateCommand(
    long Id,
    DateTime StartDate,
    DateTime EndDate,
    decimal? HourlyRate,
    decimal? DailyRate,
    decimal? ServiceRate,
    decimal? VolumeRate
    ) : ICommand<FixAssetMachineryRate>;