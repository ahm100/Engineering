using Engineering.Application.Services.FiduciaryProducts.Commands.FiduciaryProductWarehouseStatusChanger;
using Engineering.Application.Services.RequestGoodsSupplyDetails;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductExitForConsumeStatusChenger;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductExitForRelocationStatusChenger;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Infra.MessageBus.Receivers;

public class WarehouseInvoiceResponseMessageConsumerDefinition : ConsumerDefinition<WarehouseInvoiceResponseMessageConsumer>
{
    public WarehouseInvoiceResponseMessageConsumerDefinition()
    {
        EndpointName = $"warehouse-invoice-response-message-fiduciaryProduct";
        ConcurrentMessageLimit = 10;
    }

#pragma warning disable CS0672 // Member overrides obsolete member
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<WarehouseInvoiceResponseMessageConsumer> consumerConfigurator)
    {
        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitMqReceiveEndpointConfigurator)
        {
        }
    }
#pragma warning restore CS0672 // Member overrides obsolete member
}

public class WarehouseInvoiceResponseMessageConsumer : IConsumer<WarehouseInvoiceResponse>
{
    private readonly ILogger<WarehouseInvoiceResponseMessageConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResiliencePipelineProvider<string> _resilience;
    private readonly IRequestGoodsSupplyDetailLogic _goodsSupplyLogic;
    private readonly IUserProfileService _userProfileService;
    private readonly long _currenctUserId;

    public WarehouseInvoiceResponseMessageConsumer(
        ILogger<WarehouseInvoiceResponseMessageConsumer> logger,
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ResiliencePipelineProvider<string> resilience,
        IRequestGoodsSupplyDetailLogic goodsSupplyLogic,
        IUserProfileService userProfileService)
    {
        _logger = logger;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _resilience = resilience;
        _goodsSupplyLogic = goodsSupplyLogic;
        _userProfileService = userProfileService;
        _currenctUserId = _userProfileService.GetProfileInfo().UserId;
    }

    public async Task Consume(ConsumeContext<WarehouseInvoiceResponse> context)
    {
#pragma warning disable CS0168 // Variable is declared but never used
        try
        {
            if (context.Message.Status != WarehouseInvoiceStatus.New && context.Message.Status != WarehouseInvoiceStatus.Pending)
            {
                var commercialRequestId = context.Message.CommercialRequestId;
                if (!commercialRequestId.HasValue || commercialRequestId is null)
                    throw new Exception("warehouse - commercialRequestId is null");

                var invoiceId = context.Message.Id;
                var invoiceType = context.Message.InvoiceType;
                var invoiceParentId = context.Message.ParentId;
                var status = context.Message.Status;
                var userId = context.Message.UpdaterId;
                var description = context.Message.Description;
                var statusDescription = context.Message.ChangeStatusDescription;
                var type = context.Message.InvoiceType;
                var products = context.Message.Products;
                var warehouseId = context.Message.WarehouseId;
                var requestProducts = context.Message.RequestProducts;
                var managementGoodsSupplies = context.Message.ManagementGoodsSupplies;
                if (invoiceParentId is not null && invoiceParentId.HasValue)
                    invoiceId = invoiceParentId.Value;

                if (context.Message.InvoiceType == WarehouseInvoiceType.EntryThroughBorrow || context.Message.InvoiceType == WarehouseInvoiceType.ExitForBorrow)
                {
                    if (status == WarehouseInvoiceStatus.Rejected || status == WarehouseInvoiceStatus.Returned || status == WarehouseInvoiceStatus.Approved || status == WarehouseInvoiceStatus.IncompleteDelivered)
                    {
                        var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");
                        var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new FiduciaryProductWarehouseStatusChangerCommand(commercialRequestId!.Value,
                            invoiceId, status, type, statusDescription, userId, products, requestProducts, _mediator, _currenctUserId), context.CancellationToken), context.CancellationToken);
                        if (changeStatus.Value is null)
                            throw new Exception($"warehouse - FiduciaryProducts with id {commercialRequestId} not found");

                        await _unitOfWork.CommitAsync(context.CancellationToken);
                    }
                }
                else if (status == WarehouseInvoiceStatus.Approved || status == WarehouseInvoiceStatus.Rejected ||
                        status == WarehouseInvoiceStatus.Returned || status == WarehouseInvoiceStatus.IncompleteDelivered)
                {
                    RequestGoodsSupplyProduct? supplyProduct = null;
                    if (invoiceType == WarehouseInvoiceType.ExitForConsume || invoiceType == WarehouseInvoiceType.EntryThroughRelocation)
                    {
                        var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");
                        var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(
                            new GoodsSupplyProductExitForConsumeStatusChengerCommand(
                                commercialRequestId!.Value, invoiceId, warehouseId, status, description, statusDescription,
                                products, requestProducts, managementGoodsSupplies, userId), context.CancellationToken), context.CancellationToken);
                        if (changeStatus.Value is null)
                            throw new Exception($"warehouse - request goods supply with id {commercialRequestId} not found");
                        supplyProduct = changeStatus.Value;

                        await _unitOfWork.CommitAsync(context.CancellationToken);
                    }
                    else if (invoiceType == WarehouseInvoiceType.ExitForRelocation)
                    {
                        var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");
                        var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(
                            new GoodsSupplyProductExitForRelocationStatusChengerCommand(
                                commercialRequestId!.Value, invoiceId, warehouseId, status, description, statusDescription,
                                products, requestProducts, managementGoodsSupplies, userId), context.CancellationToken), context.CancellationToken);
                        if (changeStatus.Value is null)
                            throw new Exception($"warehouse - request goods supply with id {commercialRequestId} not found");
                        supplyProduct = changeStatus.Value;

                        await _unitOfWork.CommitAsync(context.CancellationToken);
                    }

                    if (supplyProduct is not null)
                        if (GSDSRules.AllowStatusForSendNotification.Any(x => x.Equals(supplyProduct.Status)))
                            await _goodsSupplyLogic.SendNotification(supplyProduct?.CreatorId.ToString(), supplyProduct?.LastDescription, context.CancellationToken);
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
#pragma warning restore CS0168 // Variable is declared but never used
    }
}