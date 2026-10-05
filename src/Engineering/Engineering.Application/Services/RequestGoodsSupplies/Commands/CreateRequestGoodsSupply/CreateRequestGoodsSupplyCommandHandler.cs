using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.EngineeringConfigs.Queries.GetActiveConfig;
using Engineering.Domain.Entities.EngineeringConfig;
using Engineering.Domain.Entities.EngineeringConfig.Enum;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Dtos;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Commands.CreateRequestGoodsSupply;

public class CreateRequestGoodsSupplyCommandHandler : ICommandHandler<CreateRequestGoodsSupplyCommand, RequestGoodsSupply>
{
    private readonly ILogger<CreateRequestGoodsSupplyCommandHandler> _logger;
    private readonly IRequestGoodsSupplyRepository _repository;
    private readonly IMediator _mediator;

    public CreateRequestGoodsSupplyCommandHandler(ILogger<CreateRequestGoodsSupplyCommandHandler> logger,
        IRequestGoodsSupplyRepository repository,
        IMediator mediator)
    {
        _logger = logger;
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Result<RequestGoodsSupply?>> Handle(
        CreateRequestGoodsSupplyCommand request, CT ct)
    {
        try
        {
            var status = GoodsSupplyStatus.Created;
            if (request.IsDraft)
                status = GoodsSupplyStatus.Draft;

            string? configCode = null;
            var engConfig = await _mediator.Send(new GetActiveConfigQuery(), ct);
            if (!engConfig.IsBad() && engConfig.Value is not null && engConfig.Value!.HaveCodingAlgorithm)
            {
                EngineeringCodingConfig? codingConfig = null;
                switch (request.Type)
                {
                    case GoodsSupplyType.Products:
                        codingConfig = engConfig.Value!.EngineeringCodingConfigs.FirstOrDefault(x => x.Type == CodingAlgorithmType.ProductRGS);
                        break;
                    case GoodsSupplyType.Services:
                        codingConfig = engConfig.Value!.EngineeringCodingConfigs.FirstOrDefault(x => x.Type == CodingAlgorithmType.ServiceRGS);
                        break;
                    case GoodsSupplyType.Advertisements:
                        codingConfig = engConfig.Value!.EngineeringCodingConfigs.FirstOrDefault(x => x.Type == CodingAlgorithmType.AdsRGS);
                        break;
                    case GoodsSupplyType.ProjectItems:
                        codingConfig = engConfig.Value!.EngineeringCodingConfigs.FirstOrDefault(x => x.Type == CodingAlgorithmType.ProjectRGS);
                        break;
                    default:
                        break;
                }
                if (codingConfig is not null)
                {
                    var lastSerial = await _repository.GetLastCodeSerialByPrefix(
                        codingConfig.Prefix,
                        ct);

                    configCode = $"{codingConfig.Prefix}{lastSerial + 1}";
                }
            }

            var entity = RequestGoodsSupply.Create(new CreateRGSParameters
            {
                Project = null,
                ProjectOperation = request.ProjectOperation,
                ProjectOperationDetail = request.ProjectOperationDetail,
                OperationInfoSeason = request.OperationInfoSeason,

                Type = request.Type,
                Status = status,
                IsProjectSupply = false,
                ConfigCode = configCode,
                SupplierId = request.SupplyerId,
                BuyerId = request.BuyerId,
                CompanyId = request.CompanyId,

                CurrencyId = request.CurrencyId,
                TransferPrice = request.TransferPrice,
                OtherPrice = request.OtherPrice,
                DiscountOnInvoicePercentage = request.DiscountOnInvoicePercentage,
                DiscountOnInvoiceNumber = request.DiscountOnInvoiceNumber,
                DiscountedPriceOnInvoice = request.DiscountedPriceOnInvoice,
                TaxOnInvoicePercentage = request.TaxOnInvoicePercentage,
                TaxOnInvoiceNumber = request.TaxOnInvoiceNumber,
                FinalInvoiceAmount = request.FinalInvoiceAmount,

                RequestedDate = request.RequestedDate,

                IsPettyCash = request.IsPettyCash,
                Description = request.Description,

                ConsumptionRateAndInventoryUrl = request.ConsumptionRateAndInventoryUrl,
                ConsumptionAddress = request.ConsumptionAddress,

                PurchaseLocation = request.PurchaseLocation,
                PurchaseReason = request.PurchaseReason
            });

            var result = await _repository.Create(entity, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RequestGoodsSupply>(SharedErrors.UnknownError);
        }
    }
}
