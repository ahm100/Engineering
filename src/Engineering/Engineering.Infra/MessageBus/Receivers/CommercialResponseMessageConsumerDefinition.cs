using Engineering.Application.Services.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplyDetails;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductCommercialHistory;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Commands.GoodsSupplyProductCommercialStatusChenger;
using Engineering.Application.Services.TelegramMessageHistorys;
using Gita.Backend.Shared.Domain.Enums.Commerces;
using Gita.Backend.Shared.Domain.Messages.Commerces;
using System.Transactions;

namespace Engineering.Infra.MessageBus.Receivers;

public class CommercialResponseMessageConsumerDefinition : ConsumerDefinition<CommercialResponseMessageConsumer>
{
    public CommercialResponseMessageConsumerDefinition()
    {
        EndpointName = $"commerce-request-response-message";
        ConcurrentMessageLimit = 10;
    }

#pragma warning disable CS0672 // Member overrides obsolete member
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<CommercialResponseMessageConsumer> consumerConfigurator)
    {
        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitMqReceiveEndpointConfigurator)
        {
        }
    }
#pragma warning restore CS0672 // Member overrides obsolete member
}

public class CommercialResponseMessageConsumer : IConsumer<CommerceRequestResponse>
{
    private readonly ILogger<CommercialResponseMessageConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRequestGoodsSupplyDetailLogic _requestGoodsSupplyDetailLogic;
    private readonly ResiliencePipelineProvider<string> _resilience;

    public CommercialResponseMessageConsumer(
        ILogger<CommercialResponseMessageConsumer> logger,
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ResiliencePipelineProvider<string> resilience,
        IRequestGoodsSupplyLogic requestGoodsSupplyLogic,
        ITelegramMessageHistoryLogic telegramMessageHistoryLogic,
        IRequestGoodsSupplyDetailLogic requestGoodsSupplyDetailLogic)
    {
        _logger = logger;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _resilience = resilience;
        _requestGoodsSupplyDetailLogic = requestGoodsSupplyDetailLogic;
    }

    public async Task Consume(ConsumeContext<CommerceRequestResponse> context)
    {
        try
        {
            var transactionOptions = new TransactionOptions();
            transactionOptions.IsolationLevel = IsolationLevel.ReadUncommitted;
            using (var scope = new TransactionScope(TransactionScopeOption.Required, transactionOptions, TransactionScopeAsyncFlowOption.Enabled))
            {
                var commercialRequestId = context.Message.CommercialRequestId;
                if (!commercialRequestId.HasValue || commercialRequestId is null)
                {

                    var ex = new Exception("commercial - commercialRequestId is null");
                    _logger.LogError(ex, ex.Message);
                    throw ex;
                }
                var status = context.Message.Status;

                var invoiceId = context.Message.Id;
                var userId = context.Message.UpdaterId;
                var description = context.Message.Description;
                var lastDescription = context.Message.DescriptionStatus;
                var product = context.Message.ProductId;
                var operatorAppointmentId = context.Message.OperatorAppointmentId;
                var confirmCount = context.Message.ConfirmedRequestCount;
                var costCenterId = context.Message.CostCenterId;

                if (CommerceRequestStatusRules.AllowForChangeStatus.Any(x => x == status))
                {
                    await Task.Delay(1000);
                    var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new GoodsSupplyProductCommercialStatusChengerCommand(
                        commercialRequestId!.Value,
                        invoiceId,
                        status,
                        confirmCount,
                        description,
                        lastDescription,
                        userId,
                        operatorAppointmentId), context.CancellationToken), context.CancellationToken); ;
                    if (changeStatus.IsFailure)
                    {
                        var ex = new Exception($"commercial - request goods supply with id {commercialRequestId} not found");
                        _logger.LogError(ex, ex.Message);
                        throw ex;
                    }

                    await _unitOfWork.CommitAsync(context.CancellationToken);
                }
                else if (CommerceRequestStatusRules.AllowForAddHistory.Any(x => x == status))
                {
                    await Task.Delay(1000);
                    var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new GoodsSupplyProductCommercialHistoryCommand(
                        commercialRequestId!.Value,
                        invoiceId,
                        status,
                        confirmCount,
                        description,
                        lastDescription,
                        userId,
                        operatorAppointmentId), context.CancellationToken), context.CancellationToken); ;
                    if (changeStatus.IsFailure)
                    {
                        var ex = new Exception($"commercial - request goods supply with id {commercialRequestId} not found");
                        _logger.LogError(ex, ex.Message);
                        throw ex;
                    }

                    await _unitOfWork.CommitAsync(context.CancellationToken);
                }
                scope.Complete();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}

public class CommerceRequestStatusRules
{
    public static List<CommerceRequestStatus> AllowForChangeStatus =
        [
        CommerceRequestStatus.Done,
        CommerceRequestStatus.Rejected,
        CommerceRequestStatus.PreInvoiceConfirm,
        CommerceRequestStatus.PreInvoiceUnpaid,
        CommerceRequestStatus.Return,
        CommerceRequestStatus.ReturnToSupply,
    ];

    public static List<CommerceRequestStatus> AllowForAddHistory =
        [
        CommerceRequestStatus.Confirm,
        CommerceRequestStatus.Archived,
        CommerceRequestStatus.InquiryConfirm,
        CommerceRequestStatus.EndInquiry,
        CommerceRequestStatus.PreInvoice,
        CommerceRequestStatus.PreInvoiceResend,
        CommerceRequestStatus.PreInvoiceReturn,
        CommerceRequestStatus.PreInvoiceReject,
    ];

}

//var goodsSupplyProduct = changeStatus.Value;
//TODO
//if (goodsSupplyProduct is not null)
//{
//    if (RequestGoodsSupplyProduct.AllowStatusForSendNotification.Any(x => x.Equals(goodsSupplyProduct.Status)))
//        await _requestGoodsSupplyDetailLogic.SendNotification(goodsSupplyProduct?.CreatorId.ToString(), goodsSupplyProduct?.LastDescription, context.ct);
//    if (RequestGoodsSupplyProduct.AllowStatusForRejectedMessage.Any(x => x.Equals(goodsSupplyProduct!.Status)))
//        await _requestGoodsSupplyDetailLogic.NotifyTelegramChats(goodsSupplyProduct, description, context.ct);
//}