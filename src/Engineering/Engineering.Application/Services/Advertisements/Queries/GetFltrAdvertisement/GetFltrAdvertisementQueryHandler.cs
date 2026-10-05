using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.Advertisements;
using Engineering.Application.Services.Advertisements.Contracts.GetFltrAdvertisement;
using Engineering.Domain.Errors.Advertisements;

namespace Engineering.Application.Services.Advertisements.Queries.GetFltrAdvertisement;

public class GetFltrAdvertisementQueryHandler : IQueryHandler<GetFltrAdvertisementQuery, GetFltrAdvertisementResponse?>
{
    private readonly ILogger<GetFltrAdvertisementQueryHandler> _logger;
    private readonly IAdvertisementRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetFltrAdvertisementQueryHandler(
        ILogger<GetFltrAdvertisementQueryHandler> logger,
        IAdvertisementRepository repository,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetFltrAdvertisementResponse?>> Handle(GetFltrAdvertisementQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFltrAdvertisement(
                request.FilterData,
                request.IsActive,
                request.PageIndex,
                request.PageSize, ct);

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

            return result.Data.Any() ?
                new GetFltrAdvertisementResponse(result.Data, result.RowCount) :
                Result.Failure<GetFltrAdvertisementResponse?>(AdvertisementErrors.FilteredAdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrAdvertisementResponse?>(SharedErrors.UnknownError);
        }
    }
}