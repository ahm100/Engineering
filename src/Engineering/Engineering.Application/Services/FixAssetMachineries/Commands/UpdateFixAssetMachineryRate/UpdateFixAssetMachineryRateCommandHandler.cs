using Engineering.Application.Abstractions.Data.FixAssetMachineries;
using FixAssetMachineryRate = Engineering.Domain.Entities.FixAssetMachineries.FixAssetMachineryRate;

namespace Engineering.Application.Services.FixAssetMachineries.Commands.UpdateFixAssetMachineryRate;

public class UpdateFixAssetMachineryRateCommandHandler : ICommandHandler<UpdateFixAssetMachineryRateCommand, FixAssetMachineryRate>
{
    private readonly ILogger<UpdateFixAssetMachineryRateCommand> _logger;
    private readonly IFixAssetMachineryRateRepository _repository;

    public UpdateFixAssetMachineryRateCommandHandler(ILogger<UpdateFixAssetMachineryRateCommand> logger, IFixAssetMachineryRateRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<FixAssetMachineryRate?>> Handle(UpdateFixAssetMachineryRateCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<FixAssetMachineryRate>(FixAssetMachineryErrors.RateNotfound);

            var ratesData = entity.FixAssetMachinery.FixAssetMachineryRates.Where(x => x.Id != entity.Id).ToList();
            bool overlapExists = ratesData.Any(a => request.StartDate < a.EndDate && request.EndDate > a.StartDate);
            if (overlapExists)
                return Result.Failure<FixAssetMachineryRate?>(FixAssetMachineryErrors.RatesDateIsDuplicate);

            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);
            entity.SetHourlyRate(request.HourlyRate);
            entity.SetDailyRate(request.DailyRate);
            entity.SetServiceRate(request.ServiceRate);
            entity.SetVolumeRate(request.VolumeRate);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<FixAssetMachineryRate>(SharedErrors.UnknownError);
        }
    }
}