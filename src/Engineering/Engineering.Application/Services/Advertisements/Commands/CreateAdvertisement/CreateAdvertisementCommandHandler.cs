using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Domain.Entities.Advertisements;
using Engineering.Domain.Errors.Advertisements;
using IdentityServer.ClientSdk.Services;

namespace Engineering.Application.Services.Advertisements.Commands.CreateAdvertisement;

public class CreateAdvertisementCommandHandler : ICommandHandler<CreateAdvertisementCommand, Advertisement?>
{
    private readonly ILogger<CreateAdvertisementCommandHandler> _logger;
    private readonly IAdvertisementRepository _repository;
    private readonly IUserInfoProvider _userInfoProvider;

    public CreateAdvertisementCommandHandler(ILogger<CreateAdvertisementCommandHandler> logger,
        IAdvertisementRepository repository,
        IUserInfoProvider userInfoProvider)
    {
        _logger = logger;
        _repository = repository;
        _userInfoProvider = userInfoProvider;
    }

    public async Task<Result<Advertisement?>> Handle(CreateAdvertisementCommand request, CT ct)
    {
        try
        {
            var doesTitleExists = await _repository.DoesTitleExist(null, request.TitleFa, request.TitleEn, ct);
            if (doesTitleExists is not null && doesTitleExists.Value)
                return Result.Failure<Advertisement?>(AdvertisementErrors.NameIsDuplicate);

            var companyId = _userInfoProvider.CompanyId;

            var ad = new Advertisement(request.TitleFa,
                request.TitleEn,
                request.DescriptionFa,
                request.DescriptionEn,
                request.TechnicalCode,
                request.DocumentUrls,
                companyId,
                request.IsActive);
            var create = await _repository.Create(ad, ct);
            return create;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Advertisement?>(SharedErrors.UnknownError);
        }
    }
}
