using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Currencies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGS;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetFltrRGS;

public class GetFltrRGSQueryHandler : IQueryHandler<GetFltrRGSQuery, GetFltrRGSResponse?>
{
    private readonly ILogger<GetFltrRGSQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IViewOrganizationRepository _orgRepo;
    private readonly IViewCurrencyRepository _currRepo;

    public GetFltrRGSQueryHandler(ILogger<GetFltrRGSQueryHandler> logger, IRequestGoodsSupplyRepository repository,
        IViewThirdPartyRepository thirdPartyRepo,
        IViewOrganizationRepository orgRepo,
        IViewCurrencyRepository currRepo)
    {
        _logger = logger;
        _repository = repository;
        _thirdPartyRepo = thirdPartyRepo;
        _orgRepo = orgRepo;
        _currRepo = currRepo;
    }

    public async Task<Result<GetFltrRGSResponse?>> Handle(GetFltrRGSQuery request, CT ct)
    {
        try
        {
            var requestGoods = await _repository.GetFltrRGS(request.ProjectIds,
                request.CityId,
                request.ProjectManagerId,
                request.Statuses,
                request.Types,
                request.CreatorIds,
                request.FromDate,
                request.ToDate,
                request.FilterData,
                request.PageIndex,
                request.PageSize, ct);

            if (requestGoods.Data is null || requestGoods.RowCount < 1)
                return Result.Failure<GetFltrRGSResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithFilterNotFound);

            var thirdPartyIds = requestGoods.Data
                .Select(x => x.SupplyerId)
                .Concat(requestGoods.Data.Select(x => x.BuyerId))
                .NullListed(x => x);

            var thirdParties = await _thirdPartyRepo.GetByIds(thirdPartyIds, ct);

            var orgs = await _orgRepo.GetByIds(requestGoods.Data.NullListed(x => x.RequestingOrganizationId), ct);

            var currencies = await _currRepo.GetCurrenciesByIds(requestGoods.Data.NullListed(x => x.CurrencyId), ct);

            var creators = await _thirdPartyRepo.GetByUserIds(requestGoods.Data.Listed(x => x.CreatorId), ct);

            foreach (var item in requestGoods.Data)
            {
                item.Supplyer = thirdParties.FirstOrDefault(x => x.Id == item.SupplyerId)?.FullName;
                item.Buyer = thirdParties.FirstOrDefault(x => x.Id == item.BuyerId)?.FullName;
                item.Currency = currencies?.FirstOrDefault(x => x.Id == item.CurrencyId)?.Name;
                item.RequestingOrganization = orgs.FirstOrDefault(x => x.Id == item.RequestingOrganizationId)?.NameFa;
                item.Creator = creators.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            }

            return new GetFltrRGSResponse(requestGoods.Data, requestGoods.RowCount);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetFltrRGSResponse?>(SharedErrors.UnknownError);
        }
    }
}
