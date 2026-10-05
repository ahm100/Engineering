using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCommerce;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCRHeader;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Gita.Backend.Shared.Domain.Enums.Commerces;

namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateCRHeader;

public class CreateCRHeaderCommandHandler : ICommandHandler<CreateCRHeaderCommand, CreateCRHeaderResponse?>
{
    private readonly ILogger<CreateCRHeaderCommandHandler> _logger;
    private readonly ICommercialService _commercialService;

    public CreateCRHeaderCommandHandler(ILogger<CreateCRHeaderCommandHandler> logger,
        ICommercialService commercialService)
    {
        _logger = logger;
        _commercialService = commercialService;
    }

    public async Task<Result<CreateCRHeaderResponse?>> Handle(CreateCRHeaderCommand request, CT ct)
    {
        try
        {
            var rgs = request.RequestGoodsSupply;
            var type = MapGoodsSupplyTypeToCommerceRequestType(rgs);

            var createCRHeaderReq = new CreateCRHeaderRequest
            {
                RequestNumber = null,
                Type = (int)type,
                CostCategoryId = null,
                CostGroupId = null,
                RequestGoodsSupplyId = rgs.Id,
                RequestSerialNumber = rgs.RequestSerialNumber,
                Description = rgs.Description,
                Details = []
            };

            List<CreateCRModel> details = [];

            foreach (var item in request.Details!)
            {
                var rgsType = item.RequestGoodsSupplyType;
                var rgsDetails = item.RequestGoodsSupplyTypeDetails?.Where(x => x.RequestGoodsSupplyTypeId == rgsType.Id);

                var newCRReq = new CreateCRModel
                {
                    Status = (int)CommerceRequestStatus.New,
                    PreStatus = null,
                    Type = (int)type,
                    InquiryId = null,
                    RequestNumber = null,
                    ProjectId = rgs.ProjectId,
                    RequestDate = rgs.RequestedDate ?? DateTime.Now,
                    DeliveryDate = rgsType.DelivaryDeadLine,
                    ReferenceId = rgsType.ReferenceId != 0 && rgsType.ReferenceId != null ? rgsType.ReferenceId.Value : 1,
                    RGSSupplyType = (int)rgsType.Type,
                    MainRequestCount = rgsType.RequestedCount,
                    RealRequestCount = rgsType.RequestedCount,
                    ConfirmedRequestCount = rgsType.RequestedCount,
                    Price = rgsType.TotalPrice,
                    CurrencyId = rgs.CurrencyId,
                    Description = rgs.Description,
                    RequestSerialNumber = rgsType.RequestSerialNumber,
                    CommercialRequestNumber = null,
                    SupplyerId = rgs.SupplyerId,
                    RequestedDate = rgs.RequestedDate,
                    CreatedInSupply = rgs.Created,
                    PaymentType = (int)CommercePaymentType.Payement,
                    IsManually = false,
                    CreateInvoice = false,

                    Urls = rgsType.RequestGoodsSupplyTypeDocuments
                        .Select(x => x.Url)
                        .ToList(),

                    CCDetails = rgsDetails?
                        .Select(x => new CreateCRDetailModel
                        {
                            CostCenterId = x.CostCenterId,
                            RequestCount = x.RequestedCount,
                        })
                        .ToList()
                };

                details.Add(newCRReq);
            }

            createCRHeaderReq.Details = details;

            var json = System.Text.Json.JsonSerializer.Serialize(createCRHeaderReq);

            _logger.LogInformation("CreateCRHeader request: {Json}", json);

            var create = await _commercialService.CreateCRHeader(createCRHeaderReq, ct);

            if (create.IsBad())
                return create.Failure<CreateCRHeaderResponse>()!;

            return new CreateCRHeaderResponse(create.Value!.Id, true);
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var response = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            response!.Error.StatusCode = 422;
            return Result.Failure<CreateCRHeaderResponse?>(CommercialErrors.ProviderError(response!.Error.Message!));
        }
    }

    public CommerceRequestType MapGoodsSupplyTypeToCommerceRequestType(RequestGoodsSupply goodsSupply)
    {
        if (goodsSupply.IsPettyCash is not null && goodsSupply.IsPettyCash.Value)
            return CommerceRequestType.PettyCash;

        switch (goodsSupply.Type)
        {
            case GoodsSupplyType.Contractor:
                return CommerceRequestType.Contractor;
            case GoodsSupplyType.GoodsSupply:
                return CommerceRequestType.GoodsSupply;
            case GoodsSupplyType.Project:
                return CommerceRequestType.OnProject;
            case GoodsSupplyType.Products:
                return CommerceRequestType.Products;
            case GoodsSupplyType.Services:
                return CommerceRequestType.Services;
            case GoodsSupplyType.Advertisements:
                return CommerceRequestType.Advertisements;
            case GoodsSupplyType.ProjectItems:
                return CommerceRequestType.ProjectItems;
            case GoodsSupplyType.PurchaseForContractor:
                return CommerceRequestType.PurchaseForContractor;
            default:
                throw new ArgumentOutOfRangeException(nameof(goodsSupply.Type), goodsSupply.Type, null);
        }
    }
}