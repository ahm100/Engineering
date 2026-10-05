using Engineering.Application.Abstractions.Interfaces;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.DeletePaymentOrder;

public class DeletePaymentOrderCommandHandler : ICommandHandler<DeletePaymentOrderCommand, bool>
{
    private readonly ILogger<DeletePaymentOrderCommandHandler> _logger;
    private readonly ITreasuryService _treasuryService;

    public DeletePaymentOrderCommandHandler(ILogger<DeletePaymentOrderCommandHandler> logger,
        ITreasuryService treasuryService)
    {
        _logger = logger;
        _treasuryService = treasuryService;
    }

    public async Task<Result<bool>> Handle(DeletePaymentOrderCommand request, CT ct)
    {
        try
        {
            var result = await _treasuryService.DeletePaymentOrder(new(request.Id), ct);
            if (result is null || result.IsFailure)
                return false;

            return true;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }

}
