using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Queries.GetPaymentOrdersTotalSummary;

public class GetPaymentOrdersTotalSummaryQueryHandler : IQueryHandler<GetPaymentOrdersTotalSummaryQuery, DataResult<List<GetPaymentOrdersTotalSummaryModel>>>
{
    private readonly ILogger<GetPaymentOrdersTotalSummaryQueryHandler> _logger;
    private readonly ITreasuryService _treasuryService;

    public GetPaymentOrdersTotalSummaryQueryHandler(ILogger<GetPaymentOrdersTotalSummaryQueryHandler> logger, ITreasuryService treasuryService)
    {
        _logger = logger;
        _treasuryService = treasuryService;
    }

    public async Task<Result<DataResult<List<GetPaymentOrdersTotalSummaryModel>>?>> Handle(GetPaymentOrdersTotalSummaryQuery request, CT ct)
    {
        try
        {
            var result = await _treasuryService.GetPaymentOrdersTotalSummary(request.Adapt<GetPaymentOrdersTotalSummaryRequest>(), ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetPaymentOrdersTotalSummaryModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetPaymentOrdersTotalSummaryModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetPaymentOrdersTotalSummaryModel>>>(SharedErrors.UnknownError);
        }
    }
}