using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostPaidCC;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostRecentCC;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetMonthlyPayment;
using Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyOperationCreatedByProjectReport;

namespace Engineering.Application.Services.Dashboard.Contracts.GetMainDashboard;

public record GetMainDashboardResponse(
    GetMainDashboardModel? Data,
    GetMainDashboardDetailModel? Detail
    );

public class GetMainDashboardModel
{
    public List<GetsDailyOperationCreatedByProjectReportModel>? DailyReport { get; set; }
    public List<GetMonthlyPaymentModel>? MonthlyCSSCreatedAmount { get; set; }
    public List<GetCostCentersMostPaidCCModel>? MostPaidCCModel { get; set; }
    public List<GetCostCentersMostRecentCCModel>? MostRecentCCModel { get; set; }
}

public class GetMainDashboardDetailModel
{
    public decimal CSSPaid { get; set; }
    public int DailyCount { get; set; }
    public decimal RequestSupplyCreated { get; set; }
    public decimal RequestSupplyProvided { get; set; }
}
