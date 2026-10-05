using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetFilteredPaymentOrdersSummary;
using Engineering.Application.WebServices.Treasury.PaymentOrders.Models.GetPaymentOrdersTotalSummary;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Queries.GetFilteredPaymentOrdersSummary;

public class GetFilteredPaymentOrdersSummaryQueryHandler : IQueryHandler<GetFilteredPaymentOrdersSummaryQuery, DataResult<List<GetFilteredPaymentOrdersSummaryModel>>>
{
    private readonly ILogger<GetFilteredPaymentOrdersSummaryQueryHandler> _logger;
    private readonly ITreasuryService _treasuryService;

    public GetFilteredPaymentOrdersSummaryQueryHandler(ILogger<GetFilteredPaymentOrdersSummaryQueryHandler> logger, ITreasuryService treasuryService)
    {
        _logger = logger;
        _treasuryService = treasuryService;
    }

    public async Task<Result<DataResult<List<GetFilteredPaymentOrdersSummaryModel>>?>> Handle(GetFilteredPaymentOrdersSummaryQuery request, CT ct)
    {
        try
        {
            var newReq = new GetFilteredPaymentOrdersSummaryRequest()
            {
                MetaThirdPartyId = request.MetaThirdPartyId,
                ProjectId = request.ProjectId,
                Type = 3,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
            var result = await _treasuryService.GetFilteredPaymentOrdersSummary(newReq, ct);

            return (result?.Value?.Data?.Any()) ?? false ?
                new DataResult<List<GetFilteredPaymentOrdersSummaryModel>>
                {
                    Data = result.Value.Data,
                    RowCount = result.Value.RowCount
                } : Result.Failure<DataResult<List<GetFilteredPaymentOrdersSummaryModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetFilteredPaymentOrdersSummaryModel>>>(SharedErrors.UnknownError);
        }
    }
}