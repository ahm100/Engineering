using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Commercial.Commerces.Models.CreateCommerce;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;
using Gita.Backend.Shared.Domain.Enums.Commerces;

namespace Engineering.Application.WebServices.Commercial.Commerces.Commands.CreateTypeCommerce;

public class CreateTypeCommerceCommandHandler : ICommandHandler<CreateTypeCommerceCommand, CreateCommerceResponse?>
{
    private readonly ILogger<CreateTypeCommerceCommandHandler> _logger;
    private readonly ICommercialService _commercialService;

    public CreateTypeCommerceCommandHandler(ILogger<CreateTypeCommerceCommandHandler> logger,
        ICommercialService commercialService)
    {
        _logger = logger;
        _commercialService = commercialService;
    }

    public async Task<Result<CreateCommerceResponse?>> Handle(CreateTypeCommerceCommand request, CT ct)
    {
        try
        {
            var supplyProduct = request.SupplyType!;
            var supply = request.RequestGoodsSupply!;
            long? operatorAppointmentId = null;
            long? thirdPartyId = null;
            long? categoryId = null;
            long? branchId = null;
            long? seasoneId = null;
            if (supply.OperationInfoSeason is not null)
            {
                categoryId = supply.OperationInfoSeason.Season.Branch.Category.Id;
                branchId = supply.OperationInfoSeason.Season.Branch.Id;
                seasoneId = supply.OperationInfoSeason.Season.Id;
            }

            if (supply.Type == GoodsSupplyType.Project)
            {
                operatorAppointmentId = supply.BuyerId;
                thirdPartyId = supply.SupplyerId;
            }
            else if (supply.Type == GoodsSupplyType.Contractor)
            {
                operatorAppointmentId = supplyProduct.ContractorId;
                thirdPartyId = supplyProduct.ContractorId;
            }

            CreateCommerceRequestRequestInquieyModel? inquery = null;
            if (supply.Type == GoodsSupplyType.Project || supply.Type == GoodsSupplyType.Contractor)
            {
                var unitPrice = supplyProduct.UnitPrice is null || supplyProduct.UnitPrice == 0 ? supplyProduct.PackageUnitPrice : supplyProduct.UnitPrice;
                var transferPrice = supply.TransferPrice / supply.RequestGoodsSupplyProducts.Count();
                var otherPrice = supply.OtherPrice / supply.RequestGoodsSupplyProducts.Count();
                var newDetails = new CreateCommerceRequestRequestModel(
                    null,
                    supplyProduct.PackageId,
                    request.RequestCount,
                    unitPrice,
                    supplyProduct.TotalPrice,
                    null,
                    supplyProduct.FinalPrice - transferPrice - supplyProduct.PackingPrice,
                    null,
                    null,
                    supplyProduct.PackageUnitPrice,
                    false);

                inquery = new CreateCommerceRequestRequestInquieyModel(
                    null,
                    thirdPartyId,
                    supply.CurrencyId,
                    null,
                    supplyProduct.PackingPrice ?? 0,
                    transferPrice,
                    otherPrice,
                    [newDetails]);
            }

            var commerceType = MapGoodsSupplyTypeToCommerceRequestType(supply);
            var result = await _commercialService.CreateCommerce(new CreateCommerceRequest(
                request.Id,
                (int)commerceType,
                request.CostCenterId,
                request.ProjectId,
                request.ProjectOperationId,
                request.ProjectOperationDetailIds,
                supplyProduct.ReferenceId ?? 1,
                request.RequestCount,
                supplyProduct.TotalPrice,
                supply.CurrencyId,
                request.Description,
                supplyProduct.DelivaryDeadLine,
                operatorAppointmentId,
                supplyProduct.CreatorId,
                supply.Id,
                supplyProduct.Id,
                $"{supplyProduct.SerialNumber}-{supplyProduct.Id}",
                inquery,
                request.Documents,
                request.CreateInvoice,
                false,
                supplyProduct.PackageId,
                request.CommerceDestinationWarehouseId,
                supplyProduct.CheckGroup,
                null,
                supplyProduct.Description,
                supplyProduct.ManagementDescription,
                supply.SupplyerId,
                request.ConfirmUserId,
                null,
                categoryId,
                branchId,
                seasoneId,
                supply.BuyerId,
                supply.RequestedDate,
                supply.Created,
                supply.RequestSerialNumber
                ), ct);
            if (result is null || result.IsFailure)
                return Result.Failure<CreateCommerceResponse>(CommercialErrors.ProviderError(result?.Error));

            return result!.Value;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var response = JsonConvert.DeserializeObject<FailureModel>(ex.Content!);
            response!.Error.StatusCode = 422;
            return Result.Failure<CreateCommerceResponse?>(CommercialErrors.ProviderError(response!.Error.Message!));
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
