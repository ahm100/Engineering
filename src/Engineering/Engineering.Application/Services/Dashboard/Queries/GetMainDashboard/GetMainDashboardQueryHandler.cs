using Engineering.Application.Services.ContractorContracts.Queries.GetCostCentersMostPaidCC;
using Engineering.Application.Services.ContractorContracts.Queries.GetCostCentersMostRecentCC;
using Engineering.Application.Services.ContractorStatusStatements.Queries.GetMonthlyPayment;
using Engineering.Application.Services.ContractorStatusStatements.Queries.GetTodaysPayment;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyCreatedCount;
using Engineering.Application.Services.DailyProjectOperations.Queries.GetsDailyOperationCreatedByProjectReport;
using Engineering.Application.Services.Dashboard.Contracts.GetMainDashboard;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetDailyRequestProductsCount;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetDailyRequestProvidedProductsCount;

namespace Engineering.Application.Services.Dashboard.Queries.GetMainDashboard;

public class GetMainDashboardQueryHandler : IQueryHandler<GetMainDashboardQuery, GetMainDashboardResponse?>
{
    private readonly ILogger<GetMainDashboardQueryHandler> _logger;
    private readonly IMediator _mediator;
    private readonly IUserInfoService _userInfoService;

    public GetMainDashboardQueryHandler(
        ILogger<GetMainDashboardQueryHandler> logger,
        IMediator mediator,
        IUserInfoService userInfoService)
    {
        _logger = logger;
        _mediator = mediator;
        _userInfoService = userInfoService;
    }

    public async Task<Result<GetMainDashboardResponse?>> Handle(GetMainDashboardQuery request, CT ct)
    {
        try
        {
            var companyId = CompanyValidator.GetCompanyId(_userInfoService);
            if (companyId is null ||
                await CompanyValidator.IsCompanyValid(companyId, _mediator, ct) == false)
                return Result.Failure<GetMainDashboardResponse?>(GlobalErrors.InvalidCompany);

            var cssPaid = await _mediator.Send(new GetTodaysPaymentQuery(), ct);
            var dailyCount = await _mediator.Send(new GetsDailyCreatedCountQuery(), ct);
            var requestSupplyCreated = await _mediator.Send(new GetDailyRequestProductsCountQuery(), ct);
            var requestSupplyProvided = await _mediator.Send(new GetDailyRequestProvidedProductsCountQuery(), ct);

            var dailyReport = await _mediator.Send(new GetsDailyOperationCreatedByProjectReportQuery(), ct);
            var monthlyCSSCreatedAmount = await _mediator.Send(new GetMonthlyPaymentQuery(6), ct);
            var mostPaidCCModel = await _mediator.Send(new GetCostCentersMostPaidCCQuery(companyId.Value), ct);
            var mostRecentCCModel = await _mediator.Send(new GetCostCentersMostRecentQuery(companyId.Value), ct);

            var detail = new GetMainDashboardDetailModel
            {
                CSSPaid = !cssPaid.IsBad() ? cssPaid.Value!.Value : 0,
                DailyCount = !dailyCount.IsBad() ? dailyCount.Value!.Value : 0,
                RequestSupplyCreated = !requestSupplyCreated.IsBad() ? requestSupplyCreated.Value!.Value : 0,
                RequestSupplyProvided = !requestSupplyProvided.IsBad() ? requestSupplyProvided.Value!.Value : 0
            };

            var data = new GetMainDashboardModel
            {
                DailyReport = !dailyReport.IsBad() ? dailyReport.Value!.Data : null,
                MonthlyCSSCreatedAmount = !monthlyCSSCreatedAmount.IsBad() ? monthlyCSSCreatedAmount.Value!.Data : null,
                MostPaidCCModel = !mostPaidCCModel.IsBad() ? mostPaidCCModel.Value!.Data : null,
                MostRecentCCModel = !mostRecentCCModel.IsBad() ? mostRecentCCModel.Value!.Data : null,
            };

            return new GetMainDashboardResponse(data, detail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetMainDashboardResponse?>(SharedErrors.UnknownError);
        }
    }
}
