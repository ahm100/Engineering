using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Groups;
using Engineering.Application.Services.Advertisements.Queries.GetAdvertisementByIds;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetRGSTypeByRGSId;

public class GetRGSTypeByRGSIdQueryHandler : IQueryHandler<GetRGSTypeByRGSIdQuery, GetRGSTypeByRGSIdResponse?>
{
    private readonly ILogger<GetRGSTypeByRGSIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyTypeRepository _repository;
    private readonly IViewProductRepository _productRepo;
    private readonly IViewGroupRepository _grpRepo;
    private readonly IMeasureUnitRepository _measureRepo;
    private readonly IMediator _mediator;

    public GetRGSTypeByRGSIdQueryHandler(ILogger<GetRGSTypeByRGSIdQueryHandler> logger,
        IRequestGoodsSupplyTypeRepository repository,
        IViewProductRepository productRepo,
        IViewGroupRepository grpRepo,
        IMeasureUnitRepository measureRepo,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _productRepo = productRepo;
        _grpRepo = grpRepo;
        _measureRepo = measureRepo;
        _mediator = mediator;
    }

    public async Task<Result<GetRGSTypeByRGSIdResponse?>> Handle(GetRGSTypeByRGSIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetRGSTypeByRGSId(request.Id,
                request.PageIndex,
                request.PageSize, ct);

            if (result.Data is null || result.RowCount < 1)
                return Result.Failure<GetRGSTypeByRGSIdResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithFilterNotFound);

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

            var productDict = products?.ToDictionary(x => x.Id) ?? [];
            var serviceDict = services?.Value?.Data?.ToDictionary(x => x.Id) ?? [];
            var adDict = ads?.Value?.Data?.ToDictionary(x => x.Id) ?? [];

            var groupIds = products?.Listed(x => x.Group.Id);
            var groups = await _grpRepo.GetByIds(groupIds ?? [], ct);
            List<MeasureUnit>? measures = [];
            if (products is not null && products.Count > 0)
            {
                if (groups is not null && groups.Count > 0)
                    measures = await _measureRepo.GetMeasureUnitsByIds(groups.Listed(x => x.MeasureUnitId), ct);
            }

            foreach (var detail in detailModels)
            {
                switch (detail.Type)
                {
                    case SupplyType.Product:
                        if (productDict.TryGetValue(detail.ReferenceId!.Value, out var product))
                            detail.ReferenceName = product?.Name;
                        detail.ReferenceNameEn = product?.NameEn;
                        detail.ReferenceCode = product?.Code;
                        var measureId = groups?.FirstOrDefault(x => x.Id == product?.Group.Id)?.MeasureUnitId;
                        detail.Measure = measures.FirstOrDefault(x => x.Id == measureId)?.Name;
                        break;

                    case SupplyType.Service:
                        if (serviceDict.TryGetValue(detail.ReferenceId!.Value, out var service))
                            detail.ReferenceName = service?.ServiceInfoName;
                        detail.ReferenceNameEn = service?.ServiceInfoEnName;
                        detail.ReferenceCode = service?.ServiceInfoCode;
                        detail.Measure = "خدماتی";
                        break;

                    case SupplyType.Ads:
                        if (adDict.TryGetValue(detail.ReferenceId!.Value, out var ad))
                            detail.ReferenceName = ad?.TitleFa;
                        detail.ReferenceNameEn = ad?.TitleEn;
                        detail.ReferenceCode = ad?.TechnicalCode;
                        detail.Measure = "تبلیغاتی";
                        break;

                    case SupplyType.Project:
                        detail.ReferenceName = detail.ProjectTypeModel?.ProjectName;
                        detail.ReferenceNameEn = detail.ProjectTypeModel?.ProjectEnName;
                        detail.ReferenceCode = detail.ProjectTypeModel?.ProjectCode;
                        detail.Measure = "پروژه";
                        break;
                }
            }

            return new GetRGSTypeByRGSIdResponse(detailModels, result.RowCount);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetRGSTypeByRGSIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
