using Engineering.Application.Services.RequestMachineryStatusStatements.Commands.PaymentRequestMachineryStatusChange;
using Engineering.Application.Services.TransportationRequests.Commands.PaymentSnapChange;
using Engineering.Application.Services.TransportationRequests.Commands.PaymentTransportationStatusChange;

namespace Engineering.Infra.MessageBus.Receivers;

public class TreasuryPettyCashResponseMessageConsumerDefinition : ConsumerDefinition<TreasuryPettyCashResponseMessageConsumer>
{
    public TreasuryPettyCashResponseMessageConsumerDefinition()
    {
        EndpointName = $"treasury-change-status-pettycash";
        ConcurrentMessageLimit = 10;
    }

#pragma warning disable CS0672 // Member overrides obsolete member
    protected override void ConfigureConsumer(
        IReceiveEndpointConfigurator endpointConfigurator,
        IConsumerConfigurator<TreasuryPettyCashResponseMessageConsumer> consumerConfigurator)
    {
        if (endpointConfigurator is IRabbitMqReceiveEndpointConfigurator rabbitMqReceiveEndpointConfigurator)
        {
        }
    }
#pragma warning restore CS0672 // Member overrides obsolete member
}

public class TreasuryPettyCashResponseMessageConsumer : IConsumer<PettyCashBillContentResponse>
{
    private readonly ILogger<TreasuryPettyCashResponseMessageConsumer> _logger;
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ResiliencePipelineProvider<string> _resilience;

    public TreasuryPettyCashResponseMessageConsumer(
        ILogger<TreasuryPettyCashResponseMessageConsumer> logger,
        IMediator mediator,
        IUnitOfWork unitOfWork,
        ResiliencePipelineProvider<string> resilience)
    {
        _logger = logger;
        _mediator = mediator;
        _unitOfWork = unitOfWork;
        _resilience = resilience;
    }

    public async Task Consume(ConsumeContext<PettyCashBillContentResponse> context)
    {
#pragma warning disable CS0168 // Variable is declared but never used
        try
        {
            var refrenceId = context.Message.ReferenceId;
            var isDeleted = context.Message.IsDeleted;

            var pipeline = _resilience.GetPipeline<Result>("RetryPipeline");

            //RequestMachinery
            if (context.Message.PaymentOrderTypeCode == "4")
            {
                if (isDeleted == true)
                {
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new PaymentRequestMachineryStatusChangeCommand(
                        refrenceId!.Value,
                        null,
                        isDeleted
                        ), context.CancellationToken), context.CancellationToken);
                    if (changeStatus.Value is null)
                        throw new Exception($"Treasury - RequestMachinery with id {refrenceId} not found");
                }
            }

            //Snap
            if (context.Message.PaymentOrderTypeCode == "31")
            {
                if (isDeleted == true)
                {
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new PaymentSnapChangeCommand(
                    refrenceId!.Value,
                    null,
                    isDeleted
                    ), context.CancellationToken), context.CancellationToken);
                    if (changeStatus.Value is null)
                        throw new Exception($"Treasury - Snap with id {refrenceId} not found");
                }
            }

            //TransportationRequest
            if (context.Message.PaymentOrderTypeCode == "6")
            {
                if (isDeleted == true)
                {
                    var changeStatus = await pipeline.ExecuteAsync(async cancellation => await _mediator.Send(new PaymentTransportationStatusChangeCommand(
                    refrenceId!.Value,
                    null,
                    isDeleted
                    ), context.CancellationToken), context.CancellationToken);
                    if (changeStatus.Value is null)
                        throw new Exception($"Treasury - TransportationRequest with id {refrenceId} not found");
                }
            }

            await _unitOfWork.CommitAsync(context.CancellationToken);
        }
        catch (Exception ex)
        {
            throw;
        }
#pragma warning restore CS0168 // Variable is declared but never used
    }
}

