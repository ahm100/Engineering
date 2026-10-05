using Engineering.Application.Services.ContractorStatusStatements.Commands.PaymentStatusStatementStatusChange;
using Engineering.Application.Services.RequestMachineryStatusStatements.Commands.PaymentRequestMachineryStatusChange;
using Engineering.Application.Services.TransportationRequests.Commands.PaymentSnapChange;
using Engineering.Application.Services.TransportationRequests.Commands.PaymentTransportationStatusChange;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.PaymentOrderMessage;

namespace Engineering.Infra.MessageBus.Receivers;

public class TreasuryPaymentOrderResponseMessageConsumerDefinition : ConsumerDefinition<TreasuryPaymentOrderResponseMessageConsumer>
{
    public TreasuryPaymentOrderResponseMessageConsumerDefinition()
    {
        EndpointName = $"treasury-change-status-paymentOrder-transportation";
        ConcurrentMessageLimit = 10;
    }

#pragma warning disable CS0672 // Member overrides obsolete member
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TreasuryPaymentOrderResponseMessageConsumer> consumerConfigurator)
    {
        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitMqReceiveEndpointConfigurator)
        {
        }
    }
#pragma warning restore CS0672 // Member overrides obsolete member
}

public class TreasuryPaymentOrderResponseMessageConsumer : IConsumer<PaymentOrderMessageResponse>
{
    private readonly ILogger<TreasuryPaymentOrderResponseMessageConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResiliencePipelineProvider<string> _resilience;

    public TreasuryPaymentOrderResponseMessageConsumer(
        ILogger<TreasuryPaymentOrderResponseMessageConsumer> logger,
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ResiliencePipelineProvider<string> resilience)
    {
        _logger = logger;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _resilience = resilience;
    }

    public async Task Consume(ConsumeContext<PaymentOrderMessageResponse> context)
    {
#pragma warning disable CS0168 // Variable is declared but never used
        try
        {
            var paymentOrderId = context.Message.Id;
            var status = context.Message.Status;
            var refrenceId = context.Message.ReferenceId;
            var filledAmount = context.Message.FilledAmount;
            var ct = context.CancellationToken;

            if (ValidatePaymentOrderStatus.AllowForChangeStatus.Any(x => x == status))
            {
                var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");

                //Contractorstatusstatement
                if (context.Message.PaymentOrderTypeCode == "5")
                {
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new PaymentStatusStatementStatusChangeCommand(
                        refrenceId!.Value, paymentOrderId, filledAmount, status), ct), ct);
                    if (changeStatus.Value is null)
                        throw new Exception($"Treasury - ContractorStatusStatement with id {refrenceId} not found");
                }

                //RequestMachinery
                if (context.Message.PaymentOrderTypeCode == "4")
                {
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new PaymentRequestMachineryStatusChangeCommand(
                        refrenceId!.Value, status, false), ct), ct);
                    if (changeStatus.Value is null)
                        throw new Exception($"Treasury - RequestMachinery with id {refrenceId} not found");
                }

                //Snap
                if (context.Message.PaymentOrderTypeCode == "31")
                {
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new PaymentSnapChangeCommand(
                        refrenceId!.Value, status, false), ct), ct);
                    if (changeStatus.Value is null)
                        throw new Exception($"Treasury - Snap with id {refrenceId} not found");
                }

                //TransportationRequest
                if (context.Message.PaymentOrderTypeCode == "6")
                {
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new PaymentTransportationStatusChangeCommand(
                        refrenceId!.Value, status, false), ct), ct);
                    if (changeStatus.Value is null)
                        throw new Exception($"Treasury - TransportationRequest with id {refrenceId} not found");
                }

                await _unitOfWork.CommitAsync(ct);
            }
        }
        catch (Exception ex)
        {
            throw;
        }
#pragma warning restore CS0168 // Variable is declared but never used
    }
}

