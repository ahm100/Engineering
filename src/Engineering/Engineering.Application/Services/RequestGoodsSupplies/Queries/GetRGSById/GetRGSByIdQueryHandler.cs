using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Currencies;
using Engineering.Application.Abstractions.Data.Synonyms.Meta.Organizations;
using Engineering.Application.Services.Advertisements.Queries.GetAdvertisementByIds;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSById;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRGSById;

public class GetRGSByIdQueryHandler : IQueryHandler<GetRGSByIdQuery, GetRGSByIdResponse?>
{
    private readonly ILogger<GetRGSByIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly IViewThirdPartyRepository _thprepo;
    private readonly IViewCurrencyRepository _currRepo;
    private readonly IViewOrganizationRepository _orgRepo;
    private readonly IViewProductRepository _productRepo;
    private readonly IMediator _mediator;

    public GetRGSByIdQueryHandler(ILogger<GetRGSByIdQueryHandler> logger,
        IRequestGoodsSupplyRepository repository,
        IViewThirdPartyRepository thprepo,
        IViewCurrencyRepository currRepo,
        IViewOrganizationRepository orgRepo,
        IViewProductRepository productRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _thprepo = thprepo;
        _currRepo = currRepo;
        _orgRepo = orgRepo;
        _productRepo = productRepo;
        _mediator = mediator;
    }

    public async Task<Result<GetRGSByIdResponse?>> Handle(GetRGSByIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetRGSById(request.Id, ct);

            if (response is null)
                return Result.Failure<GetRGSByIdResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            List<long?> userIds = [response.BuyerId, response.SupplyerId, response.CreatorId];
            var users = await _thprepo.GetByIds(userIds.NullListed(x => x), ct);
            if (users is not null)
            {
                var buyer = users.FirstOrDefault(x => x.Id == response.BuyerId);
                var supplyer = users.FirstOrDefault(x => x.Id == response.BuyerId);
                var creator = users.FirstOrDefault(x => x.Id == response.BuyerId);
                response.Creator = creator?.FirstName + " " + creator?.LastName;
                response.Supplyer = supplyer?.FirstName + " " + supplyer?.LastName;
                response.Buyer = buyer?.FirstName + " " + buyer?.LastName;
            }

            if (response.CurrencyId != null)
            {
                var curr = await _currRepo.GetById(response.CurrencyId.Value, ct);
                response.Currency = curr.Name;
            }

            if (response.RequestingOrganizationId is not null)
            {
                var org = await _orgRepo.GetById(response.RequestingOrganizationId.Value, ct);
                response.RequestingOrganization = org?.NameFa;
            }

            var detailModels = response.DetailModels ?? [];

            var productIds = detailModels
                .Where(x => x.Type == SupplyType.Product)
                .NullListed(x => x.ReferenceId);

            var serviceIds = detailModels
                .Where(x => x.Type == SupplyType.Service)
                .NullListed(x => x.ReferenceId);

            var adIds = detailModels
                .Where(x => x.Type == SupplyType.Ads)
                .NullListed(x => x.ReferenceId);

            var products = productIds.Count > 0
                ? await _productRepo.GetProductByIds(productIds, ct)
                : [];

            var services = serviceIds.Count > 0
                ? await _mediator.Send(new GetsServiceInfoByIdsQuery(1, serviceIds.Count, serviceIds), ct)
                : null;

            var ads = adIds.Count > 0
                ? await _mediator.Send(new GetAdvertisementByIdsQuery(adIds, 1, adIds.Count), ct)
                : null;

            var productDict = products?.ToDictionary(x => x.Id) ?? [];
            var serviceDict = services?.Value?.Data?.ToDictionary(x => x.Id) ?? [];
            var adDict = ads?.Value?.Data?.ToDictionary(x => x.Id) ?? [];

            foreach (var detail in detailModels)
            {
                switch (detail.Type)
                {
                    case SupplyType.Product:
                        if (productDict.TryGetValue(detail.ReferenceId!.Value, out var product))
                            detail.ReferenceName = product.Name;
                            detail.ReferenceNameEn = product?.NameEn;
                            detail.ReferenceCode = product?.Code;
                        break;

                    case SupplyType.Service:
                        if (serviceDict.TryGetValue(detail.ReferenceId!.Value, out var service))
                            detail.ReferenceName = service.ServiceInfoName;
                        detail.ReferenceNameEn = service?.ServiceInfoEnName;
                        detail.ReferenceCode = service?.ServiceInfoCode;
                        break;

                    case SupplyType.Ads:
                        if (adDict.TryGetValue(detail.ReferenceId!.Value, out var ad))
                            detail.ReferenceName = ad.TitleFa;
                        detail.ReferenceCode = ad?.TechnicalCode;
                        break;
                }
            }

            return response;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetRGSByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
