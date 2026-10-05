using Engineering.Application.Abstractions.Data;
using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Abstractions.Data.Synonyms.Warhouse.Groups;
using Engineering.Application.Services.Advertisements.Queries.GetAdvertisementByIds;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSId;
using Engineering.Application.Services.ServiceInfos.Queries.GetsByIds;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;
using Engineering.Domain.Entities.Synonyms.Warehouse.Groups;

namespace Engineering.Application.Services.RequestGoodsSupplies.Queries.GetDetailByRGSId;

public class GetDetailByRGSIdQueryHandler : IQueryHandler<GetDetailByRGSIdQuery, GetDetailByRGSIdResponse?>
{
    private readonly ILogger<GetDetailByRGSIdQueryHandler> _logger;
    private readonly IRequestGoodsSupplyTypeDetailRepository _repository;
    private readonly IRequestGoodsSupplyTypeRepository _typeRepo;
    private readonly IMediator _mediator;
    private readonly IViewProductRepository _productRepo;
    private readonly IViewGroupRepository _grpRepo;
    private readonly IMeasureUnitRepository _measureRepo;
    private readonly IViewThirdPartyRepository _thirdPartyRepo;

    public GetDetailByRGSIdQueryHandler(ILogger<GetDetailByRGSIdQueryHandler> logger,
        IRequestGoodsSupplyTypeDetailRepository repository,
        IRequestGoodsSupplyTypeRepository typeRepo,
        IMediator mediator,
        IViewProductRepository productRepo,
        IViewGroupRepository grpRepo,
        IMeasureUnitRepository measureRepo,
        IViewThirdPartyRepository thirdPartyRepo)
    {
        _logger = logger;
        _repository = repository;
        _typeRepo = typeRepo;
        _mediator = mediator;
        _productRepo = productRepo;
        _grpRepo = grpRepo;
        _measureRepo = measureRepo;
        _thirdPartyRepo = thirdPartyRepo;
    }

    public async Task<Result<GetDetailByRGSIdResponse?>> Handle(GetDetailByRGSIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetDetailByRGSId(request.Id,
                request.PageIndex,
                request.PageSize, ct);

            if (response.Data is null || response.RowCount < 1)
                return Result.Failure<GetDetailByRGSIdResponse?>(RequestGoodsSupplyErrors.RequestGoodsSupplyWithIdNotFound);

            var creatorIds = response.Data.Listed(x => x.CreatorId);
            var creators = await _thirdPartyRepo.GetByUserIds(creatorIds, ct);

            var productIds = response.Data
                .Where(x => x.Type == SupplyType.Product)
                .NullListed(x => x.ReferenceId);

            var serviceIds = response.Data
                .Where(x => x.Type == SupplyType.Service)
                .NullListed(x => x.ReferenceId);

            var adIds = response.Data
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

            List<ViewGroup>? groups = [];
            if (products is not null && products.Count > 0)
                groups = await _grpRepo.GetByIds(products.Listed(x => x.Group.Id), ct);

            List<MeasureUnit>? measures = [];
            if(groups is not null && groups.Count > 0)
                measures = await _measureRepo.GetMeasureUnitsByIds(groups.Listed(x => x.MeasureUnitId), ct);

            foreach (var detail in response.Data)
            {
                var creator = creators.FirstOrDefault(x => x.UserId == detail.CreatorId);
                detail.Creator = creator?.FirstName + " " + creator?.LastName;
                detail.CreatorEnName = creator?.FirstNameEn + " " + creator?.LastNameEn;
                switch (detail.Type)
                {
                    case SupplyType.Product:
                        if (productDict.TryGetValue(detail.ReferenceId.Value, out var product))
                        {
                            var group = groups.FirstOrDefault(x => x.Id == product.Group.Id);
                            var measure = measures.FirstOrDefault(x => x.Id == group?.MeasureUnitId);
                            detail.Reference = product.Name;
                            detail.ReferenceEn = product.NameEn;
                            detail.ReferenceCode = product.Code;
                            detail.Measure = measure?.Name;
                            detail.MeasureEn = measure?.NameEn;
                        }
                        break;

                    case SupplyType.Service:
                        if (serviceDict.TryGetValue(detail.ReferenceId.Value, out var service))
                        {
                            detail.Reference = service.ServiceInfoName;
                            detail.ReferenceEn = service.ServiceInfoEnName;
                            detail.ReferenceCode = service.ServiceInfoCode;
                            detail.Measure = "خدماتی";
                            detail.MeasureEn = "Service";
                        }
                        break;

                    case SupplyType.Ads:
                        if (adDict.TryGetValue(detail.ReferenceId.Value, out var ad))
                        {
                            detail.Reference = ad.TitleFa;
                            detail.ReferenceEn = ad.TitleEn;
                            detail.ReferenceCode = ad.TechnicalCode;
                            detail.Measure = "تبلیغاتی";
                            detail.MeasureEn = "Ads";
                        }
                        break;

                    case SupplyType.Project:
                        var type = await _typeRepo.GetById(detail.RequestGoodsSupplyTypeId, ct);
                        detail.Reference = type.ProjectName;
                        detail.ReferenceEn = type.ProjectEnName;
                        detail.ReferenceCode = type.ProjectCode;
                        detail.Measure = "پروژه";
                        detail.MeasureEn = "Project";
                        break;
                }
            }

            return new GetDetailByRGSIdResponse(response.Data, response.RowCount);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<GetDetailByRGSIdResponse?>(SharedErrors.UnknownError);
        }
    }
}