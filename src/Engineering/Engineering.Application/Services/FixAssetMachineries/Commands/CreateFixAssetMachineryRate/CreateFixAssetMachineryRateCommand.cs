using FixAssetMachinery = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachinery;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryRate;

public record CreateFixAssetMachineryRateCommand(
    FixAssetMachinery FixAssetMachinery,
    List<CreateRateDataCommand> Rates
    ) : ICommand<bool?>;

public record CreateRateDataCommand(
    DateTime StartDate,
    DateTime EndDate,
    decimal? HourlyRate,
    decimal? DailyRate,
    decimal? ServiceRate,
    decimal? VolumeRate
);