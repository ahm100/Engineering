using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Services.Advertisements.Contracts.UpdateAdvertisement;
using Engineering.Domain.Errors.Advertisements;

namespace Engineering.Application.Services.Advertisements.Commands.UpdateAdvertisement;

public class UpdateAdvertisementCommandHandler : ICommandHandler<UpdateAdvertisementCommand, UpdateAdvertisementResponse?>
{
    private readonly ILogger<UpdateAdvertisementCommandHandler> _logger;
    private readonly IAdvertisementRepository _repository;

    public UpdateAdvertisementCommandHandler(ILogger<UpdateAdvertisementCommandHandler> logger,
        IAdvertisementRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<UpdateAdvertisementResponse?>> Handle(UpdateAdvertisementCommand request, CT ct)
    {
        try
        {
            var ad = await _repository.GetById(request.Id, ct);
            if (ad is null)
                return Result.Failure<UpdateAdvertisementResponse?>(AdvertisementErrors.AdWithIdNotFound);

            var doesTitleExists = await _repository.DoesTitleExist(ad.Id, request.TitleFa, request.TitleEn, ct);
            if (doesTitleExists is not null && doesTitleExists.Value)
                return Result.Failure<UpdateAdvertisementResponse>(AdvertisementErrors.NameIsDuplicate);

            ad.Update(request.TitleFa,
                request.TitleEn,
                request.DescriptionFa,
                request.DescriptionEn,
                request.TechnicalCode,
                request.DocumentUrls,
                request.IsActive);
            await _repository.Update(ad);
            return new UpdateAdvertisementResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<UpdateAdvertisementResponse?>(SharedErrors.UnknownError);
        }
    }
}