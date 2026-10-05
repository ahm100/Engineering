using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.CreateFixAssetMachineryRate;

public class CreateFixAssetMachineryRateCommandHandler : ICommandHandler<CreateFixAssetMachineryRateCommand, bool?>
{
    private readonly ILogger<CreateFixAssetMachineryRateCommand> _logger;
    private readonly IFixAssetMachineryRateRepository _repository;

    public CreateFixAssetMachineryRateCommandHandler(ILogger<CreateFixAssetMachineryRateCommand> logger, IFixAssetMachineryRateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(CreateFixAssetMachineryRateCommand request, CT ct)
    {
        try
        {
            if (request.FixAssetMachinery.StartDate is not null && request.FixAssetMachinery.EndDate is not null)
                if (!request.Rates.Any(x => x.StartDate.Date >= request.FixAssetMachinery.StartDate.Value.Date && x.EndDate.Date <= request.FixAssetMachinery.EndDate.Value.Date))
                    return Result.Failure<bool?>(FixAssetMachineryErrors.RatesDateIsNotValid);

            var ratesData = request.FixAssetMachinery.FixAssetMachineryRates.Where(x => x.IsDeleted == false).ToList();
            bool overlapExists = ratesData.Any(a => request.Rates.Any(b => a.StartDate < b.EndDate && a.EndDate > b.StartDate));
            if (overlapExists)
                return Result.Failure<bool?>(FixAssetMachineryErrors.RatesDateIsDuplicate);

            foreach (var item in request.Rates)
            {
                var newFixAssetMachineryRate = new FixAssetMachineryRate(
                    item.StartDate,
                    item.EndDate,
                    item.HourlyRate,
                    item.DailyRate,
                    item.ServiceRate,
                    item.VolumeRate,
                    request.FixAssetMachinery);
                var result = await _repository.Create(newFixAssetMachineryRate, ct);
            }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}