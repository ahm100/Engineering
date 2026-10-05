using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementById;
using Engineering.Domain.Errors.Advertisements;

namespace Engineering.Application.Services.Advertisements.Queries.GetAdvertisementById;

public class GetAdvertisementByIdQueryHandler : IQueryHandler<GetAdvertisementByIdQuery, GetAdvertisementByIdResponse?>
{
    private readonly ILogger<GetAdvertisementByIdQueryHandler> _logger;
    private readonly IAdvertisementRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetAdvertisementByIdQueryHandler(
        ILogger<GetAdvertisementByIdQueryHandler> logger,
        IAdvertisementRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetAdvertisementByIdResponse?>> Handle(GetAdvertisementByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetAdvertisementById(
                request.Id, ct);

            if (result == null)
                return Result.Failure<GetAdvertisementByIdResponse>(AdvertisementErrors.AdWithIdNotFound);

            var creatorId = result?.CreatorId;
            if (creatorId is not null)
            {
                var creatorData = await _thirdPartyRepo.GetByUserIds([creatorId.Value], ct);
                var creator = creatorData?.FirstOrDefault();
                result.Creator = creator?.FirstName + " " + creator?.LastName;
            }


            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetAdvertisementByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}