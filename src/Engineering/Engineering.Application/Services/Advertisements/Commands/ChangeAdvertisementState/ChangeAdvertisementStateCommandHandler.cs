using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Services.Advertisements.Contracts.ChangeAdvertisementState;
using Engineering.Domain.Errors.Advertisements;

namespace Engineering.Application.Services.Advertisements.Commands.ChangeAdvertisementState;

public class ChangeAdvertisementStateCommandHandler : ICommandHandler<ChangeAdvertisementStateCommand, ChangeAdvertisementStateResponse?>
{
    private readonly ILogger<ChangeAdvertisementStateCommandHandler> _logger;
    private readonly IAdvertisementRepository _repository;

    public ChangeAdvertisementStateCommandHandler(ILogger<ChangeAdvertisementStateCommandHandler> logger,
        IAdvertisementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ChangeAdvertisementStateResponse?>> Handle(ChangeAdvertisementStateCommand request, CT ct)
    {
        try
        {
            var ads = await _repository.GetByIds(request.Ids, ct);
            if (ads is null || ads.Count < 1)
                return Result.Failure<ChangeAdvertisementStateResponse?>(AdvertisementErrors.AdWithIdNotFound);

            foreach (var ad in ads)
            {
                if (request.IsActive)
                    ad.SetActive();
                else
                    ad.SetDeactivate();
                await _repository.Update(ad);
            }
            return new ChangeAdvertisementStateResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ChangeAdvertisementStateResponse?>(SharedErrors.UnknownError);
        }
    }
}