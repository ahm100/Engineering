using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.Advertisements.Queries.GetAdvertisementByIds;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetReferenceTypeHistory;

public class GetReferenceTypeHistoryQueryHandler : IQueryHandler<GetReferenceTypeHistoryQuery, GetReferenceTypeHistoryResponse?>
{
    private readonly ILogger<GetReferenceTypeHistoryQueryHandler> _logger;
    private readonly IRequestGoodsSupplyTypeRepository _repository;
    private readonly IViewProductRepository _productRepo;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;
    private readonly IMediator _mediator;

    public GetReferenceTypeHistoryQueryHandler(ILogger<GetReferenceTypeHistoryQueryHandler> logger,
        IRequestGoodsSupplyTypeRepository repository,
        IViewProductRepository productRepo,
        IViewThirdPartyRepository thirdPartyRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _productRepo = productRepo;
        _mediator = mediator;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetReferenceTypeHistoryResponse?>> Handle(GetReferenceTypeHistoryQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetReferenceTypeHistory(request.ReferenceId,
                request.Statuses,
                request.SupplyType,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetReferenceTypeHistoryResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithFilterNotFound);

            var detailModels = result.Data;

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

            var creators = await _thirdPartyRepo.GetByUserIds(detailModels.Listed(x => x.CreatorId), ct);

            var productDict = products?.ToDictionary(x => x.Id) ?? [];
            var serviceDict = services?.Value?.Data?.ToDictionary(x => x.Id) ?? [];
            var adDict = ads?.Value?.Data?.ToDictionary(x => x.Id) ?? [];

            foreach (var detail in detailModels)
            {
                detail.Creator = creators.FirstOrDefault(x => x.UserId == detail.CreatorId)?.FullName;
                switch (detail.Type)
                {
                    case SupplyType.Product:
                        if (productDict.TryGetValue(detail.ReferenceId!.Value, out var product))
                            detail.ReferenceName = product?.Name;
                        detail.ReferenceNameEn = product?.NameEn;
                        break;

                    case SupplyType.Service:
                        if (serviceDict.TryGetValue(detail.ReferenceId!.Value, out var service))
                            detail.ReferenceName = service?.ServiceInfoName;
                        detail.ReferenceNameEn = service?.ServiceInfoEnName;
                        break;

                    case SupplyType.Ads:
                        if (adDict.TryGetValue(detail.ReferenceId!.Value, out var ad))
                            detail.ReferenceName = ad?.TitleFa;
                        detail.ReferenceNameEn = ad?.TitleEn;
                        break;
                }
            }

            return new GetReferenceTypeHistoryResponse(detailModels, result.RowCount);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetReferenceTypeHistoryResponse?>(SharedErrors.UnknownError);
        }
    }
}
