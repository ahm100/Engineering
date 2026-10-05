using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Services.Advertisements.Contracts.GetAdvertisementByIds;
using Engineering.Domain.Errors.Advertisements;

namespace Engineering.Application.Services.Advertisements.Queries.GetAdvertisementByIds;

public class GetAdvertisementByIdsQueryHandler : IQueryHandler<GetAdvertisementByIdsQuery, GetAdvertisementByIdsResponse?>
{
    private readonly ILogger<GetAdvertisementByIdsQueryHandler> _logger;
    private readonly IAdvertisementRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetAdvertisementByIdsQueryHandler(
        ILogger<GetAdvertisementByIdsQueryHandler> logger,
        IAdvertisementRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetAdvertisementByIdsResponse?>> Handle(GetAdvertisementByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetAdvertisementByIds(
                request.Ids, request.PageSize, request.PageSize, ct);

            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetAdvertisementByIdsResponse>(AdvertisementErrors.AdWithIdNotFound);

            if (result.Data is not null && result.Data.Any())
            {
                var creatorIds = result.Data?.Select(x => x.CreatorId);
                if (creatorIds is not null && creatorIds.Count() > 0)
                {
                    var creators = await _thirdPartyRepo.GetByUserIds(creatorIds.Listed(x => x), ct);
                    foreach (var item in result.Data!)
                    {
                        var creator = creators.FirstOrDefault(x => x.UserId == item.CreatorId);
                        item.Creator = creator?.FirstName + " " + creator?.LastName;
                    }
                }
            }

            return result.Data!.Any() ?
                new GetAdvertisementByIdsResponse(result.Data, result.RowCount) :
                Result.Failure<GetAdvertisementByIdsResponse?>(AdvertisementErrors.FilteredAdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetAdvertisementByIdsResponse?>(SharedErrors.UnknownError);
        }
    }
}